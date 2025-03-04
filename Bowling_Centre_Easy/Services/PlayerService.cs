using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Services
{
    // The service layer orchestrates how and when to call the factory, repository, and handle user input.
    // It ensures that your application follows the separation of concerns principle.
    public class PlayerService
    {
        private readonly IPlayerRepository _playerRepo;

        public PlayerService(IPlayerRepository playerRepo)
        {
            _playerRepo = playerRepo;
        }

        // Registers a new player (could be guest or registered)
        public void RegisterPlayer(Player player)
        {
            // You could add validation logic here.
            _playerRepo.AddPlayer(player);
        }
        public Player Login(string username, string password)
        {
            // Look up the player by username.
            Player player = _playerRepo.GetPlayerByUsername(username);
            // Check if a player was found and if the member is a RegisteredMember.
            if (player != null && player.MemberInfo is RegisteredMember regMember)
            {
                // Verify the password.
                if (regMember.Password == password)
                {
                    return player;
                }
            }
            return null;
        }
        public void ClearPlayers()
        {
            _playerRepo.Clear();
        }

    }
}
