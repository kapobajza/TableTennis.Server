using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Domain.Models
{
    public class UserTeam
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public DateTime DateJoined { get; set; }
        public bool IsLeader { get; set; }
    }
}
