using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Services;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class CheckStatsCommand : ICommand
    {
        private readonly MemberService _memberService;

        public CheckStatsCommand(MemberService memberService)
        {
            _memberService = memberService;
        }

        public void Execute()
        {
            Console.WriteLine("Please log in to check your game stats.");
            // LoginMember() is a method in MemberService that prompts for credentials.
            Player player = _memberService.LoginMember();
            if (player == null)
            {
                Console.WriteLine("Login failed.");
                return;
            }

            // Now call the CheckStats method (also in MemberService) to display stats.
            _memberService.CheckStats(player);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
