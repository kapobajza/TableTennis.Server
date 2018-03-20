using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTennis.Domain.DTOs;
using TableTennis.Domain.Models;

namespace TableTennis.Domain.Repository.Abstract
{
    public interface IUserTeamRepo : IRepo<UserTeam>
    {
        List<TeamOfUsers> GetTeamsOfUsers(int userId, int skip, int take);
        void AddTeamAndUserTeam(Team team, UserTeam userTeam);
        List<TeamOfUsers> GetUserTeams(int userId);
    }
}
