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
    public class ExceptionLogsRepo : IDisposable
    {
        private EFContext _ctx;

        public ExceptionLogsRepo()
        {
            _ctx = new EFContext();
        }

        public bool Add(ExceptionLog entity)
        {
            return _ctx.ExceptionLogs.Add(entity) != null;
        }

        public IEnumerable<ExceptionLog> AddRange(IEnumerable<ExceptionLog> entities)
        {
            return _ctx.ExceptionLogs.AddRange(entities);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Edit(ExceptionLog entity)
        {
            _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<ExceptionLog> GetAll()
        {
            return _ctx.ExceptionLogs.ToList();
        }

        public ExceptionLog Remove(ExceptionLog entity)
        {
            return _ctx.ExceptionLogs.Remove(entity);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
