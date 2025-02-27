using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Entities;

namespace Bowling_Centre_Easy.Repos
{
    // A repository is how to separate HOW you store and retrieve data from the rest of your application.
    // Seperation of Concerns, Flexbility and Testing Positives.
    public class PlayerRepo
    {
        private List<Player> _players = new List<Player>();

        // Accessible ANYWHERE in the application and does not return a value(void).
        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public Player GetPlayerByUsername(string username)
        {
            return _players.FirstOrDefault(p => p.MemberInfo.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Player> GetAll()
        {
            return _players;
        }

        public bool RemovePlayer(Player player)
        {
            return _players.Remove(player);
        }

        public void Clear()
        {
            _players.Clear();
        }
    }
}
