using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Repos;
using Bowling_Centre_Easy.Services;

namespace Bowling_Centre_Easy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setup repositories and services
            PlayerRepo playerRepo = new PlayerRepo();

            LaneRepo laneRepo = new LaneRepo();

            MatchRepo matchRepo = new MatchRepo();

            PlayerService playerService = new PlayerService(playerRepo);

            MatchService matchService = new MatchService(matchRepo, laneRepo);

            // Note to self BowlingEngine uses laneRepo directly to get lane input from the user.
            BowlingEngine engine = new BowlingEngine(playerService, laneRepo, matchService);

            // Start the game. The engine handles player registration, match creation, etc.
            engine.StartGame();

            // Keep the console open until a key is pressed.
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
