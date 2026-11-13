using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services{
    public class UsernameExists : IUsernameExists
    {
        private readonly IDb _db;

        public UsernameExists(IDb db)
        {
            _db = db;
        }

        public Task<bool> ExistsAsync(string username, int? exceptUserId = null)
        {
            username = username.Trim();

            var query = _db.Query<User>()
                .Where(u => u.UserName == username);

            if (exceptUserId.HasValue)
            {
                query = query.Where(u => u.Id != exceptUserId.Value);
            }

            return _db.AnyAsync(query);
        }
    }
}