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

        public bool Exists(string username, int? exceptUserId = null)
        {
            var query = _db.Query<User>()
                .Where(u => u.UserName == username);

            if (exceptUserId.HasValue)
            {
                query = query.Where(u => u.Id != exceptUserId.Value);
            }

            return query.Any();
        }
    }
}