using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Entities
{
    public class GuestMember : IMember
    {
        public string Name { get; set; }
        public int GamesWon { get; set; }
    }
}
