using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Services
{
    public class MemberService
    {
        private readonly PlayerRepo _playerRepo;

        public MemberService(PlayerRepo playerRepo)
        {
            _playerRepo = playerRepo;
        }

        // CREATE: Registers a new member (player).
        public void RegisterMember(Player player)
        {
            // You can add any additional validation here if needed.
            _playerRepo.AddPlayer(player);
        }

        // READ: Retrieves a member (player) by username.
        public Player GetMemberByUsername(string username)
        {
            return _playerRepo.GetPlayerByUsername(username);
        }

        // UPDATE: Updates a member's information.
        public bool UpdateMember(Player updatedPlayer)
        {
            // Retrieve the existing player by username.
            Player existingPlayer = _playerRepo.GetPlayerByUsername(updatedPlayer.MemberInfo.Name);
            if (existingPlayer != null)
            {
                // For RegisteredMember, update email, password, etc.
                if (existingPlayer.MemberInfo is RegisteredMember existingReg &&
                    updatedPlayer.MemberInfo is RegisteredMember updatedReg)
                {
                    existingReg.Email = updatedReg.Email;
                    existingReg.Password = updatedReg.Password;
                    // Additional properties can be updated here.
                    return true;
                }
            }
            return false;
        }

        // DELETE: Deletes a member by username.
        public bool DeleteMember(string username)
        {
            Player player = _playerRepo.GetPlayerByUsername(username);
            if (player != null)
            {
                return _playerRepo.RemovePlayer(player);
            }
            return false;
        }

        // Clears all players (if needed for resetting state).
        public void ClearMembers()
        {
            _playerRepo.Clear();
        }
    }
}
