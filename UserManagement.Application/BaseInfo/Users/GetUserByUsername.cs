
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetUserByUsername : IGetUserByUsername
    {
        private readonly IDb _db;

        public GetUserByUsername(IDb db)
        {
            _db = db;
        }

        public User GetByUsername(string username)
        {
            return _db.Query<User>()
            .SingleOrDefault(u => u.UserName == username);
        }
    }
}
