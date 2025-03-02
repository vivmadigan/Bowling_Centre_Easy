using Bowling_Centre_Easy.Command_Entities;
using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Repos;
using Bowling_Centre_Easy.Services;
using Bowling_Centre_Easy.MenuOptions;

namespace Bowling_Centre_Easy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setup repositories and services.
            PlayerRepo playerRepo = new PlayerRepo();
            LaneRepo laneRepo = new LaneRepo(); // Get rid of this. 
            MatchRepo matchRepo = new MatchRepo();

            PlayerService playerService = new PlayerService(playerRepo);
            MatchService matchService = new MatchService(matchRepo);
            MemberService memberService = new MemberService(playerRepo);

            // The BowlingEngine orchestrates the main game flow
            BowlingEngine engine = new BowlingEngine(playerService, laneRepo, matchService);

            // A list of meny options, each linking an ID, a description, and a command to execute.
            List<MainMenu> menuOptions = new List<MainMenu>
            {
                new MainMenu
                {
                    Id = 1, Description = "Register to become a member",
                    Command = new RegisterUserCommand(memberService)
                },
                new MainMenu 
                { 
                    Id = 2, Description = "Start playing",
                    Command = new StartGameCommand(engine)
                },
                new MainMenu
                {
                    Id = 3, Description = "Check your game stats", 
                    Command = new CheckStatsCommand(memberService)
                },
                new MainMenu
                {
                    Id = 4, Description = "Delete your membership", 
                    Command = new DeleteMembershipCommand(memberService)
                },
                new MainMenu
                {
                    Id = 5, Description = "Update your member details", 
                    Command = new UpdateMemberCommand(memberService)
                },
            };
            // Intro lines to display before showing the menu.

            List<string> menuWelcome = new List<string>
            {
                "Welcome to Nackademin Bowling Center\n",
                "You can play either as a guest or a member\n",
                "Please enter a number to choose an option you would like:",
            };
            // Main loop: Display the menu, read input, execute commands, or exit
            bool exitProgram = false;
            while (!exitProgram)
            {
                foreach (var line in menuWelcome)
                {
                    Console.WriteLine(line);

                }

                foreach (var option in menuOptions)
                {
                    Console.WriteLine($"{option.Id} - {option.Description}");
                }

                Console.WriteLine($"{menuOptions.Count + 1} - Exit this program");


                // Read user input and check if it's a valid integer.
                // If invalid, keep prompting until the user enters a valid integer.
                int userResponse;
                while (!int.TryParse(Console.ReadLine(), out userResponse))
                {
                    Console.WriteLine("Invalid number, please try again.");
                }

                if (userResponse == menuOptions.Count + 1)
                {
                    Console.WriteLine("Exiting program. Goodbye!");
                    exitProgram = true;
                }

                else
                {
                    var selectedOption = menuOptions
                        .FirstOrDefault(x => x.Id == userResponse);

                    if (selectedOption == null)
                    {
                        Console.WriteLine("Invalid option selected. Please try again.");
                    }
                    else
                    {
                        // Safely execute the command only if it's not null
                        // The ?. operator calls Execute() only when Command != null
                        selectedOption.Command?.Execute();
                    }
                }

                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}

