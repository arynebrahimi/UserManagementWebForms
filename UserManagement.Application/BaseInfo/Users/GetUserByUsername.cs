using System.Linq;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Persons.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetUserByUsername : IGetUserByUsername
    {
        private readonly ISearchUsers _searchUsers;
        private readonly ISearchPersons _searchPersons;

        public GetUserByUsername(
            ISearchUsers searchUsers,
            ISearchPersons searchPersons)
        {
            _searchUsers = searchUsers;
            _searchPersons = searchPersons;
        }

        public UserDto GetByUsername(string username)
        {
            var user = _searchUsers.Execute(
                new ISearchUsers.Request
                {
                    Username = username
                })
                .SingleOrDefault(u => u.UserName == username);

            if (user == null)
                return null;

            var person = _searchPersons.Respond(
                new ISearchPersons.Request
                {
                    Id = user.Id
                })
                .SingleOrDefault();

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
                Person = person == null ? null : new PersonDto
                {
                    FullName = person.FullName,
                    Email = person.Email
                }
            };
        }
    }
}
