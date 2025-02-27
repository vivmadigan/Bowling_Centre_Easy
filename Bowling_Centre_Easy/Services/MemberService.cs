using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Factories;
using Bowling_Centre_Easy.Interfaces;
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
        public void RegisterMember()
        {
            Console.WriteLine("Registering new users...");
            List<Player> newPlayers = RegisterNewMembers();
            // Optionally, display a summary of the new users.
        }
        // Method to register a new member (and return the created players).
        private List<Player> RegisterNewMembers()
        {
            var players = new List<Player>();
            bool addMore = true;

            while (addMore)
            {
                Console.WriteLine("Please enter your username:");
                string userName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(userName))
                {
                    Console.WriteLine("Username cannot be empty. Please enter your username:");
                    userName = Console.ReadLine();
                }

                Console.WriteLine("Please enter your password (must be at least 6 characters):");
                string userPassword = Console.ReadLine();
                while (userPassword.Length < 6)
                {
                    Console.WriteLine("Password is too short, please try again:");
                    userPassword = Console.ReadLine();
                }

                Console.WriteLine("Please enter your email:");
                string userEmail = Console.ReadLine();
                while (!IsValidEmail(userEmail))
                {
                    Console.WriteLine("Invalid email format, please enter a valid email:");
                    userEmail = Console.ReadLine();
                }

                // Use the factory to create a registered member.
                IMember newMember = MemberFactory.CreateMember("register", userName, userPassword, userEmail);
                Player newPlayer = new Player
                {
                    MemberInfo = newMember,
                    CurrentScore = 0
                };

                RegisterMember(newPlayer);
                players.Add(newPlayer);

                Console.WriteLine("Would you like to register another new user? (Y/N):");
                string choice = Console.ReadLine();
                addMore = choice.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                          choice.Equals("Yes", StringComparison.OrdinalIgnoreCase);
            }

            return players;
        }

        public Player Login(string username, string password)
        {
            Player player = _playerRepo.GetPlayerByUsername(username);
            if (player != null && player.MemberInfo is RegisteredMember regMember)
            {
                if (regMember.Password == password)
                    return player;
            }
            return null;
        }

        public Player LoginMember()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            // Call the internal Login method (using 'this' is optional).
            Player player = this.Login(username, password);
            if (player == null)
            {
                Console.WriteLine("Login failed. Please check your credentials and try again.");
            }
            return player;
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            int atIndex = email.IndexOf('@');
            int dotIndex = email.LastIndexOf('.');
            return atIndex > 0 && dotIndex > atIndex;
        }

        public bool PromptAndUpdateMember(RegisteredMember regMember)
        {
            Console.Write("Enter your email: ");
            string newEmail = Console.ReadLine();

            Console.Write("Enter new password (must be at least 6 characters): ");
            string newPassword = Console.ReadLine();

            if (newPassword.Length < 6)
            {
                Console.WriteLine("Password too short. Update aborted.");
                return false;
            }

            // Attempt to update the member's details.
            bool success = UpdateMember(regMember.MemberID, newEmail, newPassword);
            if (success)
                Console.WriteLine("Member details updated successfully.");
            else
                Console.WriteLine("Member update failed. Please check your inputs.");

            return success;
        }

        // CREATE: Registers a new member (player).
        public void RegisterMember(Player player)
        {
            _playerRepo.AddPlayer(player);
        }

       
        // UPDATE: Updates a member's information.
        // Updates a registered member's email and password using their unique MemberID.
        public bool UpdateMember(Guid memberId, string newEmail, string newPassword)
        {
            Player player = _playerRepo.GetAll().FirstOrDefault(p =>
                p.MemberInfo is RegisteredMember reg && reg.MemberID == memberId);
            if (player != null && player.MemberInfo is RegisteredMember regMember)
            {
                if (!IsValidEmail(newEmail))
                {
                    Console.WriteLine("Invalid email format.");
                    return false;
                }
                if (newPassword.Length < 6)
                {
                    Console.WriteLine("Password must be at least 6 characters.");
                    return false;
                }
                regMember.Email = newEmail;
                regMember.Password = newPassword;
                return true;
            }
            return false;
        }

        // DELETE: Deletes a member using their unique MemberID.
        public bool DeleteMember(Guid memberId)
        {
            // Find the player whose MemberInfo is a RegisteredMember with the given MemberID.
            Player player = _playerRepo.GetAll().FirstOrDefault(p =>
                p.MemberInfo is RegisteredMember reg && reg.MemberID == memberId);
            if (player != null)
            {
                return _playerRepo.RemovePlayer(player);
            }
            return false;
        }

        // Displays stats for a registered member.
        public void CheckStats(Player player)
        {
            if (player.MemberInfo is RegisteredMember reg)
            {
                Console.WriteLine($"Welcome, {reg.Name}!");
                Console.WriteLine($"Games Won: {reg.GamesWon}");
            }
            else
            {
                Console.WriteLine("Only registered members have stats. Please register to see stats.");
            }
        }
    }
}
