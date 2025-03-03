using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Logger;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class StartGameCommand : ICommand
    {
        private readonly BowlingEngine _engine;

        public StartGameCommand(BowlingEngine engine)
        {
            _engine = engine;
        }

        public void Execute()
        {
            SingletonLogger.Instance.LogInformation("User selected 'Start Game' command.");
            _engine.StartGame();
        }
    }
}
