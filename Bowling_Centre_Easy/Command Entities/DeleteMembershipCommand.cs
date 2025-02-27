using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Services;

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
            Console.Write("Enter your username to delete your membership: ");
            string username = Console.ReadLine();

            bool success = _memberService.DeleteMember(username);
            if (success)
            {
                Console.WriteLine("Membership deleted successfully.");
            }
            else
            {
                Console.WriteLine("Membership not found or deletion failed.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
