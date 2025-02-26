using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Entities
{
    public class Match
    {
        public Guid MatchID { get; set; }

        // DateTime might be good for database
        public DateTime Date { get; set; }

        //Future proofing for more than 2 players
        public List<Player> Players { get; set; } = new List<Player>();

        public BowlingLane BowlingLane { get; set; }

        public Scorecard Scorecard { get; set; }

        public Match()
        {
            MatchID = Guid.NewGuid();
        }

    }
}
