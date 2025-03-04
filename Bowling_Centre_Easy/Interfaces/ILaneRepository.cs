using Bowling_Centre_Easy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Interfaces
{
    public interface ILaneRepository
    {
        BowlingLane GetLaneByNumber(int laneNumber);
        void MarkLaneAsUsed(int laneNumber, bool inUse);
        void Clear();
    }
}
