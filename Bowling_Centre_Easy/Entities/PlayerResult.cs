using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class PlayerResult
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int FinalScore { get; set; }

        public PlayerResult()
        {
            PlayerId = Guid.NewGuid();
        }
    }
}
