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

        public void StartGame()
        {
            bool confirmed = false;
            List<Player> players = null;
            int frameCount = 0;

            // Loop until the user confirms that the information is correct.
            while (!confirmed)
            {
                // Gather player information.
                players = GetPlayersFromUser();
                // Ask for the number of frames to be played.
                frameCount = GetFrameCountFromUser();

                // Display a summary of the gathered information.
                Console.WriteLine("\nYou have registered the following players:");
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
            BowlingLane chosenLane = GetLaneFromUser(_laneRepo);

            // Create a new scorecard and pre-populate it with empty frames.
            Scorecard scorecard = new Scorecard();
            for (int i = 1; i <= frameCount; i++)
            {
                scorecard.Frames.Add(new Frame { FrameNumber = i, Score = 0 });
            }

            // Create the match using MatchService.
            Match match = _matchService.CreateMatch(players, chosenLane, scorecard);

            // Run the match.
            RunMatch(match);
        }


        // Resets the current game state by clearing the players and lanes.
        private void ClearCurrentGameState()
        {
            _playerService.ClearPlayers();
            _laneRepo.Clear();
            Console.WriteLine("Current game state has been cleared.\n");
        }

        private List<Player> GetPlayersFromUser()
        {
            var players = new List<Player>();

            Console.Write("How many players would you like to play? (Max 6 per lane): ");
            int playerCount;
            while (!int.TryParse(Console.ReadLine(), out playerCount) || playerCount < 1 || playerCount > 6)
            {
                Console.WriteLine("Please enter a valid number between 1 and 6.");
            }

            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine($"\nRegistering Player {i + 1}:");

                Console.Write("Would you like to register as a member or play as a guest? (register/guest): ");
                string memberType = Console.ReadLine();

                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                string email = null;
                if (memberType.ToLower() == "register")
                {
                    Console.Write("Enter your email to become a registered member: ");
                    email = Console.ReadLine();
                }

                // Create a member using the factory.
                IMember member = MemberFactory.CreateMember(memberType, name, email);

                // Wrap the member info in a Player object.
                Player player = new Player
                {
                    MemberInfo = member,
                    CurrentScore = 0
                };

                // Register the player using the PlayerService.
                _playerService.RegisterPlayer(player);
                players.Add(player);
            }

            return players;
        }

        private int GetFrameCountFromUser()
        {
            Console.Write("How many frames would you like to play? (Max 10 frames per match): ");
            int frameCount;
            while (!int.TryParse(Console.ReadLine(), out frameCount) || frameCount < 1 || frameCount > 10)
            {
                Console.WriteLine("Please enter a valid number between 1 and 10.");
            }
            return frameCount;
        }

        private BowlingLane GetLaneFromUser(LaneRepo laneRepo)
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

                selectedLane = laneRepo.GetLaneByNumber(laneNumber);
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

            laneRepo.MarkLaneAsUsed(selectedLane.BowlingLaneID, true);
            return selectedLane;
        }

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
            // Create a Random instance for generating scores.
            Random rand = new Random();
            Console.WriteLine("\n--- Starting Bowling Simulation ---\n");

            // Loop through each frame.
            for (int frameIndex = 0; frameIndex < match.Scorecard.Frames.Count; frameIndex++)
            {
                // Check if this is the last frame (special double round).
                bool isLastFrame = (frameIndex == match.Scorecard.Frames.Count - 1);
                Console.WriteLine($"Frame {frameIndex + 1}{(isLastFrame ? " (Double Points)" : "")}:");

                // Process each player.
                foreach (var player in match.Players)
                {
                    // Generate a random score between 1 and 10 (inclusive).
                    int score = rand.Next(1, 11);
                    if (isLastFrame)
                    {
                        score *= 2; // Double the score for the last frame.
                    }

                    // Update the player's current score.
                    player.CurrentScore += score;

                    // (Optional) You could also add the score to the player's individual frame record if you had one.
                    Console.WriteLine($"- {player.MemberInfo.Name} scored {score} points.");
                }

                // Display cumulative scores after the frame.
                Console.WriteLine("\nCumulative scores after frame {0}:", frameIndex + 1);
                foreach (var player in match.Players)
                {
                    Console.WriteLine($"- {player.MemberInfo.Name}: {player.CurrentScore}");
                }
                Console.WriteLine(); // Blank line for readability.

                // Pause for half a second before the next frame.
                System.Threading.Thread.Sleep(1000);
            }

            // Clear any previous results in the scorecard.
            match.Scorecard.Results.Clear();

            // Populate the scorecard with final results for each player.
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

            // Determine the winner (player with the highest current score).
            var winner = match.Players.OrderByDescending(p => p.CurrentScore).First();
            Console.WriteLine($"The winner is {winner.MemberInfo.Name} with {winner.CurrentScore} points!");

            // Update the winner's GamesWon field.
            winner.MemberInfo.GamesWon++;

            // Ask if they want to play again.
            Console.Write("\nWould you like to play again? (Y/N): ");
            string playAgain = Console.ReadLine();
            if (playAgain.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                playAgain.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                // Optionally, clear or reset the current game state if needed, then restart.
                // Here, we'll call StartGame() again.
                StartGame();
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
            }
        }
    }

}

