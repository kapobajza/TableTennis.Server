using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Domain.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
