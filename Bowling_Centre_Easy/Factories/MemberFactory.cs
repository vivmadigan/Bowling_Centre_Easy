using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Factories
{
    public static class MemberFactory
    {
        public static IMember CreateMember(string memberType, string name, string password, string email = null)
        {
            switch (memberType.ToLower())
            {
                case "register":
                    // Ensure email is provided for registered members.
                    if (string.IsNullOrWhiteSpace(email))
                        throw new ArgumentException("Email is required to register members.");
                    return new RegisteredMember
                    {
                        Name = name,
                        Password = password,
                        Email = email,
                        GamesWon = 0
                    };

                case "guest":
                    return new GuestMember
                    {
                        Name = name,
                        GamesWon = 0
                    };

                default:
                    throw new ArgumentException("Unknown member type");
            }
        }
    }
}
