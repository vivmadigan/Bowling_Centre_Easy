using Bowling_Centre_Easy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Interfaces
{
    public interface IPlayerRepository
    {
        void AddPlayer(Player player);
        Player GetPlayerByUsername(string username);
        IEnumerable<Player> GetAll();
        bool RemovePlayer(Player player);
        void Clear();
        void UpdatePlayer(Player player);
    }
}
