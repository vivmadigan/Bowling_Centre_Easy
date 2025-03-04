using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.EF
{
    public class EFLaneRepository : ILaneRepository
    {
        private readonly BowlingContext _context;

        public EFLaneRepository(BowlingContext context)
        {
            _context = context;
        }

        public BowlingLane GetLaneByNumber(int laneNumber)
        {
            return _context.Lanes.FirstOrDefault(lane => lane.LaneNumber == laneNumber);
        }

        public void MarkLaneAsUsed(int laneNumber, bool inUse)
        {
            var lane = _context.Lanes.FirstOrDefault(l => l.LaneNumber == laneNumber);
            if (lane != null)
            {
                lane.InUse = inUse;
                _context.SaveChanges();
            }
        }

        public void Clear()
        {
            var lanes = _context.Lanes.ToList();
            foreach (var lane in lanes)
            {
                lane.InUse = false;
            }
            _context.SaveChanges();
        }
    }
}
