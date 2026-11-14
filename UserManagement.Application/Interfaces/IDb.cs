using System.Collections.Generic;
using System.Linq;

namespace UserManagement.Application.Interfaces
{
    public interface IDb
    {
        IQueryable<T> Query<T>() where T : class;

        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool DeleteById<T>(int id) where T : class;

        int SaveChanges();
    }
}