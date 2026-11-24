using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.BaseInfo.Users;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistence.BaseInfos.Users
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

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                string keyword = request.Username.Trim();
                items = items.Where(u => u.UserName.Contains(keyword));
            }

            if (request.Person != null)
            {
                items = from user in items
                    join person in _searchPersons.Respond(request.Person)
                        on user.PersonId equals person.Id
                    select user;
            }

            return items;
        }
    }
}