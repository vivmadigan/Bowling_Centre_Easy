using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Services
{
    // The service layer orchestrates how and when to call the factory, repository, and handle user input.
    // It ensures that your application follows the separation of concerns principle.
    public class PlayerService
    {
        private readonly PlayerRepo _playerRepo;

        public PlayerService(PlayerRepo playerRepo)
        {
            _playerRepo = playerRepo;
        }

        // Registers a new player (could be guest or registered)
        public void RegisterPlayer(Player player)
        {
            // You could add validation logic here.
            _playerRepo.AddPlayer(player);
        }
        public void ClearPlayers()
        {
            _playerRepo.Clear();
        }

    }
}
