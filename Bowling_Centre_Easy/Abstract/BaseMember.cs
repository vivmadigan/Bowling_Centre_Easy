using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Abstract
{
    public abstract class BaseMember
    {
        [Key]
        public int MemberID { get; set; }        

        public string Name { get; set; }
        public int GamesWon { get; set; }
    }
}
