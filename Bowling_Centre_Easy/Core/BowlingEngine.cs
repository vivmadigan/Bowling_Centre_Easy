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

        // Constructor that accepts dependencies. This is an example of dependency injection.
        public BowlingEngine(PlayerService playerService, LaneRepo laneRepo)
        {
            _playerService = playerService;
            _laneRepo = laneRepo;
        }

        public void StartGame()
        {
            // Calls the GetPlayersFromUser() method to prompt the user for player registration details,
            // creates Player objects based on the input (using the factory and service layers),
            // and stores the resulting list of players in the 'players' variable for use in setting up the match.

            List<Player> players = GetPlayersFromUser();
            // Create a match, assign a lane, initialize scorecards, etc.
            // Let the user choose the lane.
            BowlingLane chosenLane = GetLaneFromUser(_laneRepo);
            Match match = new Match
            {
                Date = DateTime.Now,
                Players = players,
                BowlingLane = chosenLane,
                Scorecard = new Scorecard() // Create a new scorecard.

            };

            // Run Game
            RunMatch(match);
        }

        private List<Player> GetPlayersFromUser()
        {
            var players = new List<Player>();

            Console.Write("How many players would you like to play? (Max 6 per lane): ");
            int playerCount;

            // Interesting bit of code that uses TryParse to ensure the user enters a valid number.
            // If the user enters an invalid number, the loop will continue to prompt the user until a valid number is entered.
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

                // Create a member using the factory
                IMember member = MemberFactory.CreateMember(memberType, name, email);

                // Wrap the member info in a Player object
                Player player = new Player
                {
                    MemberInfo = member,
                    CurrentScore = 0
                };

                // Optionally, register the player with the service/repository here
                _playerService.RegisterPlayer(player);

                players.Add(player);
            }

            return players;
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

            // Mark the lane as in use.
            laneRepo.MarkLaneAsUsed(selectedLane.BowlingLaneID, true);
            return selectedLane;
        }
        private void RunMatch(Match match)
        {
            Console.WriteLine("Starting match...");
            Console.WriteLine($"Match ID: {match.MatchID}");
            Console.WriteLine("Players in the match:");
            foreach (var player in match.Players)
            {
                Console.WriteLine($"- {player.MemberInfo.Name}");
            }
            Console.WriteLine($"Playing on Lane: {match.BowlingLane.LaneNumber}");
            Console.ReadLine();
            // Additional match logic (score tracking, rounds, etc.) would go here.
        }

    }
}
