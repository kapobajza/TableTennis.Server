using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTennis.Domain.Models;

namespace TableTennis.Domain.DTOs
{
    public class TeamOfUsers
    {
        public Team Team { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
