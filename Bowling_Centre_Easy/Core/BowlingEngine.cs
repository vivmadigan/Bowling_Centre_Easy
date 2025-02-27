using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Factories;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Repos;
using Bowling_Centre_Easy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Core
{
    public class BowlingEngine
    {
        private readonly PlayerService _playerService;
        private readonly LaneRepo _laneRepo;
        private readonly MatchService _matchService;

        public BowlingEngine(PlayerService playerService, LaneRepo laneRepo, MatchService matchService)
        {
            _playerService = playerService;
            _laneRepo = laneRepo;
            _matchService = matchService;
        }


        // Starts the game by gathering players, frame count, and lane selection, then simulating a match.
        public void StartGame()
        {
            bool confirmed = false;
            List<Player> players = null;
            int frameCount = 0;

            // Loop until the user confirms that the entered info is correct.
            while (!confirmed)
            {
                players = GetPlayersForGame();
                frameCount = GetFrameCountFromUser();

                Console.WriteLine("\nYou have the following players:");
                foreach (var player in players)
                {
                    Console.WriteLine($"- {player.MemberInfo.Name}");
                }
                Console.WriteLine($"Number of frames to be played: {frameCount}");
                Console.Write("Is this information correct? (Y/N): ");
                string response = Console.ReadLine();
                if (response.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                    response.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    confirmed = true;
                }
                else
                {
                    Console.WriteLine("Resetting information. Please re-enter your details.\n");
                    ClearCurrentGameState();
                }
            }

            // Let the user choose a lane.
            BowlingLane chosenLane = GetLaneFromUser();

            // Create a new scorecard and pre-populate it with empty frames.
            Scorecard scorecard = new Scorecard();
            for (int i = 1; i <= frameCount; i++)
            {
                scorecard.Frames.Add(new Frame { FrameNumber = i, Score = 0 });
            }

            // Create the match via MatchService.
            Match match = _matchService.CreateMatch(players, chosenLane, scorecard);

            // Run the match simulation.
            RunMatch(match);
        }

        // Private helper: clears current game state (players and lane usage) without affecting historical match data.
        private void ClearCurrentGameState()
        {
            _playerService.ClearPlayers();
            _laneRepo.Clear();
            Console.WriteLine("Current game state has been cleared.\n");
        }

        // Collects the players for the current game. For each player, they can choose to log in or play as a guest.
        private List<Player> GetPlayersForGame()
        {
            var players = new List<Player>();
            Console.Write("How many players will be playing? (Max 6): ");
            int count = int.Parse(Console.ReadLine()); // In production code, use input validation.

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nFor Player {i + 1}: Would you like to log in or play as a guest? (L/G): ");
                string option = Console.ReadLine();
                if (option.Equals("L", StringComparison.OrdinalIgnoreCase))
                {
                    Player player = LoginPlayer();
                    if (player != null)
                        players.Add(player);
                    else
                    {
                        Console.WriteLine("Login failed. Please try again.");
                        i--; // retry this player.
                    }
                }
                else
                {
                    Player guest = CreateGuestPlayer();
                    players.Add(guest);
                }
            }

            return players;
        }

        // Prompts for the number of frames to play.
        private int GetFrameCountFromUser()
        {
            Console.Write("How many frames would you like to play? (Max 10): ");
            int frameCount = int.Parse(Console.ReadLine()); // In production, validate input.
            return frameCount;
        }

        // Prompts the user to choose a lane. Returns the selected lane.
        private BowlingLane GetLaneFromUser()
        {
            BowlingLane selectedLane = null;
            bool validChoice = false;
            while (!validChoice)
            {
                Console.Write("Enter the lane number you wish to use (1-10): ");
                int laneNumber;
                if (!int.TryParse(Console.ReadLine(), out laneNumber) || laneNumber < 1 || laneNumber > 10)
                {
                    Console.WriteLine("Please enter a valid lane number between 1 and 10.");
                    continue;
                }
                selectedLane = _laneRepo.GetLaneByNumber(laneNumber);
                if (selectedLane == null)
                {
                    Console.WriteLine("Lane not found. Try again.");
                    continue;
                }
                else if (selectedLane.InUse)
                {
                    Console.WriteLine("That lane is currently in use. Please select a different lane.");
                }
                else
                {
                    validChoice = true;
                }
            }
            _laneRepo.MarkLaneAsUsed(selectedLane.BowlingLaneID, true);
            return selectedLane;
        }

        // Simulates the match by looping through frames, generating random scores, and determining a winner.
        private void RunMatch(Match match)
        {
            Console.WriteLine("\nStarting match...");
            Console.WriteLine($"Match ID: {match.MatchID}");
            Console.WriteLine("Players in the match:");
            foreach (var player in match.Players)
            {
                Console.WriteLine($"- {player.MemberInfo.Name}");
            }
            Console.WriteLine($"Playing on Lane: {match.BowlingLane.LaneNumber}");
            Console.WriteLine($"Total Frames: {match.Scorecard.Frames.Count}");
            Console.WriteLine("Press Enter to begin the bowling...");
            Console.ReadLine();

            Random rand = new Random();
            Console.WriteLine("\n--- Starting Bowling Simulation ---\n");

            int frameCount = match.Scorecard.Frames.Count;
            for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
            {
                bool isLastFrame = (frameIndex == frameCount - 1);
                Console.WriteLine($"Frame {frameIndex + 1}{(isLastFrame ? " (Double Points)" : "")}:");

                foreach (var player in match.Players)
                {
                    int score = rand.Next(1, 11);
                    if (isLastFrame)
                        score *= 2;
                    player.CurrentScore += score;
                    Console.WriteLine($"- {player.MemberInfo.Name} scored {score} points.");
                }

                Console.WriteLine("\nCumulative scores after frame {0}:", frameIndex + 1);
                foreach (var player in match.Players)
                {
                    Console.WriteLine($"- {player.MemberInfo.Name}: {player.CurrentScore}");
                }
                Console.WriteLine();
                Thread.Sleep(1000);
            }

            // Update the Scorecard with final results.
            match.Scorecard.Results.Clear();
            foreach (var player in match.Players)
            {
                var result = new PlayerResult
                {
                    PlayerId = player.PlayerID,
                    PlayerName = player.MemberInfo.Name,
                    FinalScore = player.CurrentScore
                };
                match.Scorecard.Results.Add(result);
            }

            var winner = match.Scorecard.Results.OrderByDescending(r => r.FinalScore).First();
            Console.WriteLine($"The winner is {winner.PlayerName} with {winner.FinalScore} points!");
            var winningPlayer = match.Players.FirstOrDefault(p => p.PlayerID == winner.PlayerId);
            if (winningPlayer != null)
            {
                winningPlayer.MemberInfo.GamesWon++;
            }

            foreach (var player in match.Players)
            {
                player.CurrentScore = 0;
            }

            Console.Write("\nWould you like to play again? (Y/N): ");
            string playAgain = Console.ReadLine();
            if (playAgain.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                playAgain.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                StartGame();
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
            }
        }


        // Prompts the user to log in. Returns the Player if login is successful.
        private Player LoginPlayer()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            // Use the login method from PlayerService.
            Player player = _playerService.Login(username, password);
            if (player == null)
            {
                Console.WriteLine("Login failed. Please check your credentials and try again.");
            }
            return player;
        }

        // Creates a guest player.
        private Player CreateGuestPlayer()
        {
            Console.Write("Enter your guest name: ");
            string guestName = Console.ReadLine();
            IMember guestMember = MemberFactory.CreateMember("guest", guestName, null);
            Player guest = new Player { MemberInfo = guestMember, CurrentScore = 0 };
            _playerService.RegisterPlayer(guest);
            return guest;
        }
    }

}

