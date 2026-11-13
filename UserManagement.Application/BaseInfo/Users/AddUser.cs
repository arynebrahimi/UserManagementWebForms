using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class AddUser : IAddUser
    {
        private readonly IDb _db;

        public AddUser(IDb db)
        {
            _db = db;
        }

        public async Task AddAsync(User user)
        {
            _db.Add(user);

            await _db.SaveChangesAsync();
        }
    }
}