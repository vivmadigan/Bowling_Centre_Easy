using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.Entities
{
    public class GuestMember : IMember
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int GamesWon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
