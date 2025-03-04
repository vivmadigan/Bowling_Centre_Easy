using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class PlayerResult
    {
        [Key]
        public int PlayerResultId { get; set; }

        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int FinalScore { get; set; }

        // Foreign key: which Scorecard does this result belong to?
        public int ScorecardId { get; set; }
        public Scorecard Scorecard { get; set; }

    }
}
