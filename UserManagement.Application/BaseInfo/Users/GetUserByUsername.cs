
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

        public Task<User> GetByUsernameAsync(string username)
        {
            var query = _db.Query<User>()
                .Where(u => u.UserName == username);

            return _db.FirstOrDefaultAsync(query);
        }
    }
}
