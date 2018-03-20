using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTennis.Domain.DataContext;
using TableTennis.Domain.Models;
using TableTennis.Domain.Repository.Abstract;

namespace TableTennis.Domain.Repository.Concrete
{
    public class TeamRepo : ITeamRepo
    {
        private EFContext _ctx;

        public TeamRepo(EFContext ctx)
        {
            _ctx = ctx;
        }

        public bool Add(Team entity)
        {
            return _ctx.Teams.Add(entity) != null;
        }

        public IEnumerable<Team> AddRange(IEnumerable<Team> entities)
        {
            return _ctx.Teams.AddRange(entities);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Edit(Team entity)
        {
            _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Team> GetAll()
        {
            return _ctx.Teams.ToList();
        }

        public Team Remove(Team entity)
        {
            return _ctx.Teams.Remove(entity);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
