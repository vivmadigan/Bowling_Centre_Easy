using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class BowlingLane
    {
        public int BowlingLaneID { get; set; }
        public int LaneNumber { get; set; }
        public bool InUse { get; set; }
    }
}
