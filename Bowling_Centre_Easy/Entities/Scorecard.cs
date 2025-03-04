using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class Scorecard
    {
        [Key]
        public int ScorecardId { get; set; }
        public List<Frame> Frames { get; set; } = new List<Frame>();

        public List<PlayerResult> Results { get; set; } = new List<PlayerResult>();


    }
}
