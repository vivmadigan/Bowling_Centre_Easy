using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Logger;
using Bowling_Centre_Easy.Services;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class UpdateMemberCommand : ICommand
    {
        private readonly MemberService _memberService;

        public UpdateMemberCommand(MemberService memberService)
        {
            _memberService = memberService;
        }

        public void Execute()
        {
            SingletonLogger.Instance.LogInformation("User selected 'Update Member' command.");
            Console.WriteLine("Please log in to update your membership details.");
            // Enforce secure login by calling LoginMember() from MemberService.
            Player player = _memberService.LoginMember();
            if (player == null || !(player.MemberInfo is RegisteredMember regMember))
            {
                Console.WriteLine("Login failed or you are not a registered member.");
                return;
            }

            // Use the encapsulated prompt-and-update method in MemberService.
            bool updated = _memberService.PromptAndUpdateMember(regMember);
            if (updated)
            {
                Console.WriteLine("Your membership details have been updated.");
            }
            else
            {
                Console.WriteLine("Update failed. Please try again.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }
}
