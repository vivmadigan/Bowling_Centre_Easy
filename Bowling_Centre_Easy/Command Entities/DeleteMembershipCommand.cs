using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class DeleteMembershipCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Deleting membership...");
            // Insert membership deletion logic.
        }
    }
}
