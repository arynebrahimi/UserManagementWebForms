using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IDb _db;

        public UpdateUser(IDb db)
        {
            _db = db;
        }

        public async Task UpdateAsync(User user)
        {
            _db.Update(user);

            await _db.SaveChangesAsync();
        }
    }
}
