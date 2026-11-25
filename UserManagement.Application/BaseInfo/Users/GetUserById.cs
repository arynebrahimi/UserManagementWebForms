
using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public class GetUserById : IGetUserById
    {
        private readonly ISearchUsers _searchUsers;
        private readonly ISearchPersons _searchPersons;

        public GetUserById(
            ISearchUsers searchUsers,
            ISearchPersons searchPersons)
        {
            _searchUsers = searchUsers;
            _searchPersons = searchPersons;
        }

        public UserDto GetById(int id)
        {
            var user = _searchUsers.Execute(
                new ISearchUsers.Request
                {
                    Id = id
                }).SingleOrDefault();

            if (user == null)
                return null;

            var person = _searchPersons.Respond(
                new ISearchPersons.Request
                {
                    Id = user.PersonId
                }).SingleOrDefault();

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
