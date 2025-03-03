using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Core;
using Bowling_Centre_Easy.Interfaces;
using Bowling_Centre_Easy.Logger;
using Bowling_Centre_Easy.Services;

namespace Bowling_Centre_Easy.Command_Entities
{
    public class RegisterUserCommand : ICommand
    {
        private readonly MemberService _memberService;

        public RegisterUserCommand(MemberService memberService)
        {
            _memberService = memberService;
        }

        public void Execute()
        {
            SingletonLogger.Instance.LogInformation("User selected 'Register to become a member' command.");

            _memberService.BeginMemberRegistration();

        }
    }
}
