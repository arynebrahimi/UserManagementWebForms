using System.Data.Entity;
using System.Linq;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistence.Data
{
    public class AppDbContext : DbContext, IDb
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }

        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        public bool DeleteById<T>(int id) where T : class
        {
            var entity = Set<T>().Find(id);

            if (entity == null)
                return false;

            Set<T>().Remove(entity);

            return true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasRequired(u => u.Person)
                .WithRequiredPrincipal(p => p.User)
                .WillCascadeOnDelete(true);
        }
    }
}