using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistence.Data
{
    public class AppDbContext : DbContext, IDb
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }
        // Generic Query
        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }

        // Generic Add
        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        // Generic Update
        public void Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        // Generic Delete
        public void Delete<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        // Generic Delete By Id
        public async Task<bool> DeleteByIdAsync<T>(int Id)
            where T : class
        {
            var entity = await Set<T>().FindAsync(Id);

            if (entity == null)
                return false;

            Set<T>().Remove(entity);

            return true;
        }

        // Generic ToList
        public Task<List<T>> ToListAsync<T>(IQueryable<T> query)
        {
            return query.ToListAsync();
        }

        // Generic FirstOrDefault
        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query)
        {
            return query.FirstOrDefaultAsync();
        }

        // Generic Any
        public Task<bool> AnyAsync<T>(IQueryable<T> query)
        {
            return query.AnyAsync();
        }

        // Save Changes
        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}