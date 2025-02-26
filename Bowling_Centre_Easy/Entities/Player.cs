using Bowling_Centre_Easy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class Player
    {
        public Guid PlayerID { get; set; }
        public IMember MemberInfo { get; set; }
        public int CurrentScore { get; set; }

        public Player()
        {
            PlayerID = Guid.NewGuid();
        }
    }
}
