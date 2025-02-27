using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class RegisterUserCommand : ICommand
    {
        private readonly BowlingEngine _engine;

        public RegisterUserCommand(BowlingEngine engine)
        {
            _engine = engine;
        }

        public void Execute()
        {
            // Calls the registration method on the engine.
            _engine.RegisterUsers();
        }
    }
}
