using Bowling_Centre_Easy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Abstract;

namespace Bowling_Centre_Easy.Entities
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        public int MemberInfoMemberID { get; set; }
        //[NotMapped]
        [ForeignKey(nameof(MemberInfoMemberID))]
        public BaseMember MemberInfo { get; set; }
        public int CurrentScore { get; set; }

    }
}
