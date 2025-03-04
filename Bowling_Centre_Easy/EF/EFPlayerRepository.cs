using Bowling_Centre_Easy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bowling_Centre_Easy.EF
{
    public class EFPlayerRepository : IPlayerRepository
    {
        private readonly BowlingContext _context;

        public EFPlayerRepository(BowlingContext context)
        {
            _context = context;
        }

        public void AddPlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public Player GetPlayerByUsername(string username)
        {
            // We do .Include(...) so that MemberInfo is loaded
            return _context.Players
                .Include(p => p.MemberInfo)
                .FirstOrDefault(p => p.MemberInfo.Name == username);
        }

        public IEnumerable<Player> GetAll()
        {
            return _context.Players
                .Include(p => p.MemberInfo)
                .ToList();
        }
        // Old function
        /*public bool RemovePlayer(Player player)
        {
            _context.Players.Remove(player);
            return _context.SaveChanges() > 0;
        }*/

        public void Clear()
        {
            // Just an example if you want to remove all players
            _context.Players.RemoveRange(_context.Players);
            _context.SaveChanges();
        }
        // New Addition to help solve problem: update a player's data in the DB
        public void UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
            _context.SaveChanges();
        }

        public bool RemovePlayer(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
            return true;
        }
    }
}
