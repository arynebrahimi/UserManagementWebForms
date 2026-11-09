
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Services
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IDb _db;

        public DeleteUser(IDb db)
        {
            _db = db;
        }

        public async Task DeleteAsync(int id)
        {
            await _db.DeleteByIdAsync<UserManagement.Domain.Entities.User>(id);

            await _db.SaveChangesAsync();
        }
    }
}
