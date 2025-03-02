using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Factories;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Repos;
using Bowling_Centre_Easy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Bowling_Centre_Easy.Core
{
    /// <summary>
    /// The main engine that orchestrates player setup, lane selection,
    /// match creation, simulation, and overall flow.
    /// </summary>
    public class BowlingEngine
    {
        private readonly PlayerService _playerService;
        private readonly LaneRepo _laneRepo;
        private readonly MatchService _matchService;

        /// <summary>
        /// Constructor injecting the necessary services and repositories.
        /// </summary>
        public BowlingEngine(PlayerService playerService, LaneRepo laneRepo, MatchService matchService)
        {
            _playerService = playerService;
            _laneRepo = laneRepo;
            _matchService = matchService;
        }

        /// <summary>
        /// Primary method to initiate the game flow:
        /// 1. Gather user details (players, frames, etc.) with validation.
        /// 2. Confirm the setup with the user.
        /// 3. Choose a lane.
        /// 4. Create a match and run it.
        /// </summary>
        public void StartGame()
        {
            bool confirmed = false;
            List<Player> players = null;
            int frameCount = 0;

            while (!confirmed)
            {
                // Get a list of players from the user (login/guest).
                players = GetPlayersForGame();

                // Ask how many frames to play (1-10).
                frameCount = GetFrameCountFromUser();

                // Show the user what they've chosen.
                Console.WriteLine("\nYou have the following players:");
                foreach (var player in players)
                {
                    Console.WriteLine($"- {player.MemberInfo.Name}");
                }
                Console.WriteLine($"Number of frames to be played: {frameCount}");

                // Prompt for confirmation to proceed or re-enter details.
                Console.Write("Is this information correct? (Y/N): ");
                bool isYes = ReadYesNo();
                if (isYes)
                {
                    confirmed = true;
                }
                else
                {
                    Console.WriteLine("Resetting information. Please re-enter your details.\n");
                    ClearCurrentGameState();
                }
            }

            // Let the user pick a lane to play on.
            BowlingLane chosenLane = GetLaneFromUser();

            // Create a new scorecard and pre-populate frames.
            Scorecard scorecard = new Scorecard();
            for (int i = 1; i <= frameCount; i++)
            {
                scorecard.Frames.Add(new Frame { FrameNumber = i, Score = 0 });
            }

            // Create and run the match.
            Match match = _matchService.CreateMatch(players, chosenLane, scorecard);
            RunMatch(match);
        }

        /// <summary>
        /// Clears the current game state: players, lane usage, etc.
        /// Does NOT remove historical matches or membership data.
        /// </summary>
        private void ClearCurrentGameState()
        {
            _playerService.ClearPlayers();
            _laneRepo.Clear();
            Console.WriteLine("Current game state has been cleared.\n");
        }

        /// <summary>
        /// Collects information about how many players will participate,
        /// and whether each player logs in or plays as a guest.
        /// </summary>
        private List<Player> GetPlayersForGame()
        {
            var players = new List<Player>();

            // Ask the user for the total number of players (1-6).
            int count = ReadIntInRange("How many players will be playing? (Max 6): ", 1, 6);

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nFor Player {i + 1}: Would you like to log in or play as a guest? (L/G): ");
                string choice = ReadLoginOrGuest();

                if (choice == "L")
                {
                    // Attempt to log in
                    Player player = LoginPlayer();
                    if (player != null)
                    {
                        players.Add(player);
                    }
                    else
                    {
                        // If login fails, decrement i to retry this same "slot".
                        Console.WriteLine("Login failed. Please try again.");
                        i--;
                    }
                }
                else
                {
                    // If 'G', create a guest player
                    Player guest = CreateGuestPlayer();
                    players.Add(guest);
                }
            }

            return players;
        }

        /// <summary>
        /// Prompts for a frame count (1-10).
        /// </summary>
        private int GetFrameCountFromUser()
        {
            return ReadIntInRange("How many frames would you like to play? (Max 10): ", 1, 10);
        }

        /// <summary>
        /// Lets the user select a lane (by lane number) as long as that lane is free.
        /// If lane is in use or invalid, it prompts again.
        /// </summary>
        private BowlingLane GetLaneFromUser()
        {
            BowlingLane selectedLane = null;
            bool validChoice = false;

            while (!validChoice)
            {
                int laneNumber = ReadIntInRange("Enter the lane number you wish to use (1-10): ", 1, 10);

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

            // Mark the chosen lane as in use.
            _laneRepo.MarkLaneAsUsed(selectedLane.BowlingLaneID, true);
            return selectedLane;
        }

        /// <summary>
        /// Runs the match by simulating bowling frames, scores, and announcing winners.
        /// </summary>
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

                // Simulate each player's random score for this frame.
                foreach (var player in match.Players)
                {
                    int score = rand.Next(1, 11);

                    // If it's the last frame, double their points.
                    if (isLastFrame)
                        score *= 2;

                    player.CurrentScore += score;
                    Console.WriteLine($"- {player.MemberInfo.Name} scored {score} points.");
                }

                // Display cumulative scores after this frame.
                Console.WriteLine("\nCumulative scores after frame {0}:", frameIndex + 1);
                foreach (var player in match.Players)
                {
                    Console.WriteLine($"- {player.MemberInfo.Name}: {player.CurrentScore}");
                }
                Console.WriteLine();
                Thread.Sleep(1000);
            }

            // Clear out old results in the Scorecard (if any).
            match.Scorecard.Results.Clear();

            // Store each player's final score in the Scorecard.
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

            // Sort results in descending order by score.
            var sortedResults = match.Scorecard.Results
                .OrderByDescending(r => r.FinalScore)
                .ToList();

            // The top score is the highest final score.
            var topScore = sortedResults[0].FinalScore;

            // Find all players who have that top score.
            var winners = sortedResults.Where(r => r.FinalScore == topScore).ToList();

            if (winners.Count == 1)
            {
                // If only one winner, announce it.
                var soleWinner = winners.First();
                Console.WriteLine($"\nThe winner is {soleWinner.PlayerName} with {soleWinner.FinalScore} points!");

                // Update the winner's record of games won.
                var winningPlayer = match.Players
                    .FirstOrDefault(p => p.PlayerID == soleWinner.PlayerId);
                if (winningPlayer != null)
                {
                    winningPlayer.MemberInfo.GamesWon++;
                }
            }
            else
            {
                // Multiple winners => a tie scenario.
                Console.WriteLine("\nIt's a tie! The following players share the top score:");
                foreach (var tieResult in winners)
                {
                    Console.WriteLine($"- {tieResult.PlayerName} with {tieResult.FinalScore} points");
                    var winningPlayer = match.Players
                        .FirstOrDefault(p => p.PlayerID == tieResult.PlayerId);
                    if (winningPlayer != null)
                    {
                        winningPlayer.MemberInfo.GamesWon++;
                    }
                }
            }

            // Reset player scores so they're fresh for the next match.
            foreach (var player in match.Players)
            {
                player.CurrentScore = 0;
            }

            // Prompt if user wants to play again.
            Console.Write("\nWould you like to play again? (Y/N): ");
            bool playAgain = ReadYesNo();
            if (playAgain)
            {
                StartGame();
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
            }
        }

        /// <summary>
        /// Prompts the user to log in. 
        /// Returns a Player object if successful; null otherwise.
        /// </summary>
        private Player LoginPlayer()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            
            Player player = _playerService.Login(username, password);
            if (player == null)
            {
                Console.WriteLine("Login failed. Please check your credentials and try again.");
            }
            return player;
        }

        /// <summary>
        /// Creates and registers a guest player on the fly.
        /// </summary>
        private Player CreateGuestPlayer()
        {
            Console.Write("Enter your guest name: ");
            string guestName = Console.ReadLine();

            // Use a factory for consistent member creation.
            IMember guestMember = MemberFactory.CreateMember("guest", guestName, null);
            Player guest = new Player
            {
                MemberInfo = guestMember,
                CurrentScore = 0
            };

            // Register with PlayerService so it’s known in the system.
            _playerService.RegisterPlayer(guest);
            return guest;
        }

        //  Utility Methods for Safe User Input

        private int ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Try parse the input to an integer.
                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                    continue;
                }

                // Check if within the specified range.
                if (value < min || value > max)
                {
                    Console.WriteLine($"Please enter a number between {min} and {max}.\n");
                    continue;
                }

                return value; 
            }
        }

        private bool ReadYesNo()
        {
            while (true)
            {
                string input = Console.ReadLine().Trim().ToUpper();

                
                if (input == "Y" || input == "YES")
                    return true;

                
                if (input == "N" || input == "NO")
                    return false;

                Console.WriteLine("Invalid choice. Please type Y or N.");
            }
        }

        private string ReadLoginOrGuest()
        {
            while (true)
            {
                string input = Console.ReadLine().Trim().ToUpper();

                if (input == "L" || input == "LOGIN")
                    return "L";

                if (input == "G" || input == "GUEST")
                    return "G";


                Console.WriteLine("Invalid choice. Please type 'L' (Log in) or 'G' (Guest).");
            }
        }
    }
}
