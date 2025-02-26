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
        public int PlayerId { get; set; } // Relate scorecard to a player, if needed
        public List<Frame> Frames { get; set; } = new List<Frame>();

        public int Result { get; set; }

        public Scorecard()
        {
            ScorecardId = Guid.NewGuid();
        }

        public int CalculateTotalScore()
        {
            // Sum up frame scores. You can add logic for spares and strikes later.
            return Frames.Sum(frame => frame.Score);
        }
    }
}
