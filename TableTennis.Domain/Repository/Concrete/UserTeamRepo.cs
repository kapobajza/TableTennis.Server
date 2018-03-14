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

        public List<TeamOfUsers> GetTeamsOfUsers(int skip, int take)
        {
            return (from t in _ctx.Teams
                    join tu in _ctx.UserTeams.Include(x => x.User).Include(x => x.Team) on t.TeamId equals tu.TeamId into gj
                    from x in gj.DefaultIfEmpty()
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
