using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class CheckStatsCommand : ICommand
    {
        private readonly BowlingEngine _engine;

        public CheckStatsCommand(BowlingEngine engine)
        {
            _engine = engine;
        }

        public void Execute()
        {
            _engine.CheckStats();
        }
    }
}
