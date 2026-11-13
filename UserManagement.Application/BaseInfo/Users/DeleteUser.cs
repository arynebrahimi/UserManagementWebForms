using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class DeleteUser : IdeleteUser
    {
        private readonly IDb _db;

        public DeleteUser(IDb db)
        {
            _db = db;
        }

        public async Task DeleteAsync(int Id)
        {
            await _db.DeleteByIdAsync<UserManagement.Domain.Entities.User>(Id);

            await _db.SaveChangesAsync();
        }
    }
}
