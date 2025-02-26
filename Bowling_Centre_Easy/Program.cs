using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Repos;
using Bowling_Centre_Easy.Services;

namespace Bowling_Centre_Easy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setup repositories and services(could be replaced with a DI container later)
            PlayerRepo playerRepo = new PlayerRepo();

            LaneRepo laneRepo = new LaneRepo();

            PlayerService playerService = new PlayerService(playerRepo);

            // Instantiate the BowlingEngine, passing in necessary dependencies
            BowlingEngine engine = new BowlingEngine(playerService, laneRepo);

            // Start the game. The engine handles player registration, match creation, etc.
            engine.StartGame();

            // Keep the console open until a key is pressed.
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
