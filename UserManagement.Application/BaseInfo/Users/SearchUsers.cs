using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class SearchUsers : ISearchUsers
    {
        private readonly IDb _db;

        public SearchUsers(IDb db)
        {
            _db = db;
        }

        public IQueryable<User> Respond(ISearchUsers.Request request = null)
        {
            var items = _db.Query<User>();

            if (request == null)
                return items;

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                string username = request.Username.Trim();

                items = items.Where(i =>
                    i.UserName.Contains(username));
            }
            return items;
        }
    }
}