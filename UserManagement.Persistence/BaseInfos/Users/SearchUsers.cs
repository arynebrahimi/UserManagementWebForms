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
            var items = _db.Query<User>();

            if (request == null)
                return items;

            if (request.Id.HasValue)
                items = items.Where(u => u.Id == request.Id.Value);

            bool hasUsername = !string.IsNullOrWhiteSpace(request.Username);

            bool hasPerson = request.Person != null &&
                (request.Person.Id.HasValue ||
                 !string.IsNullOrWhiteSpace(request.Person.Keyword));

            if (hasUsername && hasPerson)
            {
                string keyword = request.Username.Trim();

                var personIds = _searchPersons
                    .Respond(request.Person)
                    .Select(p => p.Id)
                    .ToList();

                items = items.Where(u =>
                    u.UserName.Contains(keyword) ||
                    personIds.Contains(u.Id));
            }
            else if (hasUsername)
            {
                string keyword = request.Username.Trim();

                items = items.Where(u =>
                    u.UserName.Contains(keyword));
            }
            else if (hasPerson)
            {
                var personIds = _searchPersons
                    .Respond(request.Person)
                    .Select(p => p.Id)
                    .ToList();

                items = items.Where(u =>
                    personIds.Contains(u.Id));
            }

            return items;
        }
    }
}
