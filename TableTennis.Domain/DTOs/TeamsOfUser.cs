using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTennis.Domain.Models;

namespace TableTennis.Domain.DTOs
{
    public class TeamsOfUser
    {
        public string Token { get; set; }
        public User User { get; set; }
        public List<Team> Teams { get; set; }
    }
}
