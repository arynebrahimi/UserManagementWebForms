
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class UsernameExists : IUsernameExists
    {
        private readonly IDb _db;

        public UsernameExists(IDb db)
        {
            _db = db;
        }

        public Task<bool> ExistsAsync(string username)
        {
            var query = _db.Query<User>()
                .Where(u => u.UserName == username);

            return _db.AnyAsync(query);
        }
    }
}
