using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTennis.Domain.DataContext;
using TableTennis.Domain.DTOs;
using TableTennis.Domain.Models;
using TableTennis.Domain.Repository.Abstract;
using System.Data.Entity;

namespace TableTennis.Domain.Repository.Concrete
{
    public class UserTeamRepo : IUserTeamRepo
    {
        private EFContext _ctx;

        public UserTeamRepo(EFContext ctx)
        {
            _ctx = ctx;
        }

        public bool Add(UserTeam entity)
        {
            return _ctx.UserTeams.Add(entity) != null;
        }

        public IEnumerable<UserTeam> AddRange(IEnumerable<UserTeam> entities)
        {
            return _ctx.UserTeams.AddRange(entities);
        }

        public void AddTeamAndUserTeam(Team team, UserTeam userTeam)
        {
            _ctx.Teams.Add(team);
            _ctx.UserTeams.Add(userTeam);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Edit(UserTeam entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<UserTeam> GetAll()
        {
            return _ctx.UserTeams.ToList();
        }

        public List<TeamOfUsers> GetTeamsOfUsers(int userId, int skip, int take)
        {
            var teamIds = _ctx
                    .UserTeams
                    .Where(x => x.UserId == userId)
                    .Select(x => x.TeamId)
                    .ToList();

            return (from t in _ctx.Teams
                    join tu in _ctx.UserTeams.Include(x => x.User).Include(x => x.Team) on t.TeamId equals tu.TeamId into gj
                    from x in gj.DefaultIfEmpty()
                    where !teamIds.Contains(x.TeamId)
                    select new TeamOfUsers()
                    {
                        Team = t,
                        Users = gj
                           .Select(y => new UserDTO()
                           {
                               UserId = y.UserId,
                               UserName = y.User.UserName
                           })
                           .ToList()
                    })
                    .GroupBy(x => x.Team.TeamId)
                    .Select(x => new TeamOfUsers()
                    {
                        Team = x.FirstOrDefault().Team,
                        Users = x.FirstOrDefault().Users
                    })
                    .OrderByDescending(x => x.Team.DateCreated)
                    .Skip(skip)
                    .Take(take)
                    .ToList();
        }

        public List<TeamOfUsers> GetUserTeams(int userId)
        {
            var teamIds = _ctx
                    .UserTeams
                    .Where(x => x.UserId == userId)
                    .Select(x => x.TeamId)
                    .ToList();

            return _ctx
                .UserTeams
                .Include(x => x.User)
                .Include(x => x.Team)
                .Where(x => teamIds.Contains(x.TeamId))
                .GroupBy(x => x.TeamId)
                .Select(x => new TeamOfUsers()
                {
                    Team = x.FirstOrDefault().Team,
                    Users = x.Select(y => new UserDTO()
                    {
                        UserId = y.UserId,
                        UserName = y.User.UserName
                    }).ToList()
                })
                .ToList();
        }

        public UserTeam Remove(UserTeam entity)
        {
            return _ctx.UserTeams.Remove(entity);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
