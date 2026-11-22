using System.Linq;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Persons.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IDb _db;
        private readonly ISearchUsers _searchUsers;
        private readonly ISearchPersons _searchPersons;

        public UpdateUser(
            IDb db,
            ISearchUsers searchUsers,
            ISearchPersons searchPersons)
        {
            _db = db;
            _searchUsers = searchUsers;
            _searchPersons = searchPersons;
        }

        public void Update(UpdateUserDto request)
        {
            if (request == null)
                return;

            var user = _searchUsers.Execute(
                new ISearchUsers.Request { Id = request.Id })
                .SingleOrDefault();

            var person = _searchPersons.Respond(
                new ISearchPersons.Request { Id = request.Id })
                .SingleOrDefault();

            if (user == null || person == null)
                return;

            user.UserName = request.UserName.Trim();
            person.FullName = request.FullName.Trim();
            person.Email = request.Email.Trim();

            _db.Update(user);
            _db.Update(person);

            _db.SaveChanges();
        }
    }
}
