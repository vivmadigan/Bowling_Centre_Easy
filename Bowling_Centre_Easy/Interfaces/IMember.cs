using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Interfaces
{
    public interface IMember
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public int GamesWon { get; set; }
    }
}
