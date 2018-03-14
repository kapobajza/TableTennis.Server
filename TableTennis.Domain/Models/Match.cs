using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Domain.Models
{
    [Table("Matches")]
    public class Match
    {
        public int MatchId { get; set; }


        [ForeignKey("TeamOne")]
        public int TeamOneId { get; set; }
        public virtual Team TeamOne { get; set; }


        [ForeignKey("TeamTwo")]
        public int TeamTwoId { get; set; }
        public virtual Team TeamTwo { get; set; }

        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public string Duration { get; set; }
    }
}
