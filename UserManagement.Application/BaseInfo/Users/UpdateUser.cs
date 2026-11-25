using System;
using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.BaseInfo.Users
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IDb _db;
        private readonly ISearchUsers _searchUsers;
        private readonly ISearchPersons _searchPersons;
        private readonly IUsernameExists _usernameExists;

        public UpdateUser(
            IDb db,
            ISearchUsers searchUsers,
            ISearchPersons searchPersons,
            IUsernameExists usernameExists)
        {
            _db = db;
            _searchUsers = searchUsers;
            _searchPersons = searchPersons;
            _usernameExists = usernameExists;
        }

        public bool Update(UpdateUserDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                throw new Exception("Please fill in all fields.");
            }

            var user = _searchUsers.Execute(
                new ISearchUsers.Request { Id = request.Id })
                .SingleOrDefault();

            if (user == null)
                throw new Exception("User not found.");

            var person = _searchPersons.Respond(
                new ISearchPersons.Request { Id = user.PersonId })
                .SingleOrDefault();

            if (person == null)
                throw new Exception("Person not found.");

            string username = request.UserName.Trim();

            if (_usernameExists.Exists(username, request.Id))
                throw new Exception("This username is already used by another user.");

            user.UserName = username;
            person.FullName = request.FullName.Trim();
            person.Email = request.Email.Trim();

            _db.Update(user);
            _db.Update(person);
            _db.SaveChanges();

            return true;
        }
    }
}
