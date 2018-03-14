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
    public class UserRepo : IUserRepo
    {
        private EFContext _ctx;

        public UserRepo(EFContext ctx)
        {
            _ctx = ctx;
        }

        public bool Add(User entity)
        {
            return _ctx.Users.Add(entity) != null;
        }

        public IEnumerable<User> AddRange(IEnumerable<User> entities)
        {
            return _ctx.Users.AddRange(entities);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Edit(User entity)
        {
            _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<User> GetAll()
        {
            return _ctx.Users.ToList();
        }

        public User Remove(User entity)
        {
            return _ctx.Users.Remove(entity);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
