
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Application.Interfaces
{
    public interface IDb
    {
        IQueryable<T> Query<T>() where T : class;

        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> DeleteByIdAsync<T>(int id) where T : class;

        Task<List<T>> ToListAsync<T>(IQueryable<T> query);

        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query);
        Task<bool> AnyAsync<T>(IQueryable<T> query);
        Task SaveChangesAsync();
    }
}