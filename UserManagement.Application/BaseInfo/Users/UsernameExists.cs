using System.Linq;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class UsernameExists : IUsernameExists
    {
        private readonly IDb _db;

        public UsernameExists(IDb db)
        {
            _db = db;
        }

        public bool Exists(string username, int id = 0)
        {
            return _db.Query<User>()
                .Any(u => u.UserName == username && u.Id != id);
        }
    }
}