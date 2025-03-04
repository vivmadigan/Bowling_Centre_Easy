using Bowling_Centre_Easy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Repos
{
    public class LaneRepo : ILaneRepository
    {
        private List<BowlingLane> _lanes;

        public LaneRepo()
        {
            // Initialize 10 lanes.
            _lanes = new List<BowlingLane>();
            for (int i = 1; i <= 10; i++)
            {
                _lanes.Add(new BowlingLane { LaneNumber = i, InUse = false });
            }
        }

        // Returns a lane by its number, if available.
        public BowlingLane GetLaneByNumber(int laneNumber)
        {
            return _lanes.FirstOrDefault(lane => lane.LaneNumber == laneNumber);
        }

        // Mark a lane as in use or free.
        public void MarkLaneAsUsed(int laneNumber, bool inUse)
        {
            BowlingLane lane = _lanes.FirstOrDefault(l => l.BowlingLaneID == laneNumber);
            if (lane != null)
            {
                lane.InUse = inUse;
            }
        }
        public void Clear()
        {
            foreach (var lane in _lanes)
            {
                lane.InUse = false;
            }
        }
    }
}