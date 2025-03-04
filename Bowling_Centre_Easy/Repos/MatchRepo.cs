using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Repos
{
    // This repository lets you store and retrieve matches, following the same pattern as with players.
    public class MatchRepo : IMatchRepository
    {
        private List<Match> _matches = new List<Match>();

        public void Add(Match match)
        {
            _matches.Add(match);
        }

        public Match GetById(int matchId)
        {
            return _matches.FirstOrDefault(m => m.MatchID == matchId);
        }

    }
}
