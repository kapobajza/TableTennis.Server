using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Domain.Repository.Abstract
{
    public interface IRepo<T> : IDisposable
    {
        bool Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        IEnumerable<T> GetAll();
        void Edit(T entity);
        T Remove(T entity);
        void SaveChanges();
    }
}
