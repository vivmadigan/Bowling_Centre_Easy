using Bowling_Centre_Easy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.EF
{
    public class EFMatchRepository : IMatchRepository
    {
        private readonly BowlingContext _context;

        public EFMatchRepository(BowlingContext context)
        {
            _context = context;
        }

        public void Add(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public Match GetById(int matchId)
        {
            return _context.Matches.FirstOrDefault(m => m.MatchID == matchId);
        }
    }
}
