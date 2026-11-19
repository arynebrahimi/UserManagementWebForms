using System.Linq;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistence.DataAccess.Users
{
    public class SearchUsers : ISearchUsers
    {
        private readonly IDb _db;
        private readonly ISearchPersons _searchPersons;

        public SearchUsers(IDb db, ISearchPersons searchPersons)
        {
            _db = db;
            _searchPersons = searchPersons;
        }

        public IQueryable<User> Execute(ISearchUsers.Request request)
        {
            var users = _db.Query<User>();

            if (request == null)
                return users;

            if (request.Id.HasValue)
                users = users.Where(u => u.Id == request.Id.Value);

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                string keyword = request.Username.Trim();

                var personIds = _searchPersons
                    .Respond(request.Person)
                    .Select(p => p.Id)
                    .ToList();

                users = users.Where(u =>
                    u.UserName.Contains(keyword) ||
                    personIds.Contains(u.Id));
            }

            return users;
        }
    }
}