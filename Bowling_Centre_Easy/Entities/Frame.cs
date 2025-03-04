using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class Frame
    {
        [Key]
        public int FrameId { get; set; }

        public int FrameNumber { get; set; }
        public int Score { get; set; }

        // Foreign key back to Scorecard.
        // Tells EF: "Each Frame row references which Scorecard it belongs to."
        public int ScorecardId { get; set; }
        public Scorecard Scorecard { get; set; }
    }
}
