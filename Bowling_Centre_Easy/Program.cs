using Bowling_Centre_Easy.Command_Entities;
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
            MemberService memberService = new MemberService(playerRepo);

            // Create the BowlingEngine (which contains game logic).
            BowlingEngine engine = new BowlingEngine(playerService, laneRepo, matchService);

            // Create a mapping from menu option to ICommand.
            Dictionary<int, ICommand> menuCommands = new Dictionary<int, ICommand>
            {
                { 1, new RegisterUserCommand(memberService) },
                { 2, new StartGameCommand(engine) },
                { 3, new CheckStatsCommand(memberService) },
                { 4, new DeleteMembershipCommand(memberService) },
                { 5, new UpdateMemberCommand(memberService) }
            };

            List<string> menuOptions = new List<string>
            {
                "Welcome to Nackademin Bowling Center\n",
                "You can play either as a guest or a member\n",
                "Please enter a number to choose an option you would like:",
                "1 – Register to become a member",
                "2 – Start playing",
                "3 – Check your game stats",
                "4 – Delete your membership",
                "5 – Update your member details",
                "6 – Exit this program\n"
            };

            bool exitProgram = false;
            while (!exitProgram)
            {
                foreach (var line in menuOptions)
                {
                    Console.WriteLine(line);
                }

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

                if (userResponse == 6)
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

