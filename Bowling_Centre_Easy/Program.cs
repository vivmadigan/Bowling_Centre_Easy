﻿using Bowling_Centre_Easy.Command_Entities;
using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Repos;
using Bowling_Centre_Easy.Services;

namespace Bowling_Centre_Easy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setup repositories and services.
            PlayerRepo playerRepo = new PlayerRepo();
            LaneRepo laneRepo = new LaneRepo();
            MatchRepo matchRepo = new MatchRepo();

            PlayerService playerService = new PlayerService(playerRepo);
            MatchService matchService = new MatchService(matchRepo, laneRepo);

            // Create the BowlingEngine (which contains registration, game logic, etc.).
            BowlingEngine engine = new BowlingEngine(playerService, laneRepo, matchService);

            // Create a mapping from menu option to ICommand.
            Dictionary<int, ICommand> menuCommands = new Dictionary<int, ICommand>
            {
                { 1, new RegisterUserCommand(engine) },
                { 2, new StartGameCommand(engine) },
                { 3, new CheckStatsCommand(engine) },
                { 4, new DeleteMembershipCommand() }
                // Option 5 is for Exit, handled separately.
            };

            bool exitProgram = false;
            while (!exitProgram)
            {
                // Display the menu.
                Console.WriteLine("Welcome to Nackademin Bowling Center\n");
                Console.WriteLine("You can play either as a guest or a member\n");
                Console.WriteLine("Please enter a number to choose an option you would like:");
                Console.WriteLine("1 - Register to become a member");
                Console.WriteLine("2 - Start playing!");
                Console.WriteLine("3 - Check your game stats");
                Console.WriteLine("4 - Delete your membership");
                Console.WriteLine("5 - Exit this program\n");

                int userResponse = 0;
                bool validInput = false;
                while (!validInput)
                {
                    try
                    {
                        userResponse = int.Parse(Console.ReadLine());
                        validInput = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid number, please try again.");
                    }
                }

                if (userResponse == 5)
                {
                    Console.WriteLine("Exiting program. Goodbye!");
                    exitProgram = true;
                }
                else if (menuCommands.ContainsKey(userResponse))
                {
                    menuCommands[userResponse].Execute();
                }
                else
                {
                    Console.WriteLine("Invalid option selected. Please try again.");
                }

                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

