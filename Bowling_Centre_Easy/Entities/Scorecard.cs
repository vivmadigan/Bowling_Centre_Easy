using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class Scorecard
    {
        public Guid ScorecardId { get; set; }
        public List<Frame> Frames { get; set; } = new List<Frame>();

        public List<PlayerResult> Results { get; set; } = new List<PlayerResult>();

        public Scorecard()
        {
            ScorecardId = Guid.NewGuid();
        }

    }
}
