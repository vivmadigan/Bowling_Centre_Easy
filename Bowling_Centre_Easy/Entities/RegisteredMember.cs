using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Entities
{
    public class RegisteredMember : IMember
    {
        public int MemberID { get; set; }}
        public string Name { get; set; }
        public string Email { get; set; }
        public int GamesWon { get; set; }
    }
}
