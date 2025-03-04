using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Logger;
using Bowling_Centre_Easy.Services;
using Bowling_Centre_Easy.Abstract;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class DeleteMembershipCommand : ICommand
    {
        private readonly MemberService _memberService;

        public DeleteMembershipCommand(MemberService memberService)
        {
            _memberService = memberService;
        }

        public void Execute()
        {
            SingletonLogger.Instance.LogInformation("User selected 'Delete Membership' command.");
            Console.WriteLine("Please log in to delete your membership.");
            // Use the existing LoginMember method to authenticate the user.
            Player player = _memberService.LoginMember();
            if (player == null)
            {
                Console.WriteLine("Login failed.");
                return;
            }

            // Ensure the logged-in player is a registered member.
            if (player.MemberInfo is RegisteredMember regMember)
            {
                bool success = _memberService.DeleteMember(regMember.MemberID);
                if (success)
                    Console.WriteLine("Membership deleted successfully.");
                else
                    Console.WriteLine("Membership deletion failed.");
            }
            else
            {
                Console.WriteLine("Only registered members can delete their membership.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
