using Bowling_Centre_Easy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Interfaces
{
    public interface IMatchRepository
    {
        void Add(Match match);
        Match GetById(int matchId);

    }
}
