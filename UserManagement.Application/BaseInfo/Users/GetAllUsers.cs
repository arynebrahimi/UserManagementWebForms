using System.Collections.Generic;
using System.Linq;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Persons.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly ISearchUsers _searchUsers;
        private readonly ISearchPersons _searchPersons;

        public GetAllUsers(
            ISearchUsers searchUsers,
            ISearchPersons searchPersons)
        {
            _searchUsers = searchUsers;
            _searchPersons = searchPersons;
        }

        public List<UserDto> GetAll(UserSearchDto request = null)
        {
            var searchRequest = new ISearchUsers.Request();

            if (request != null &&
                !string.IsNullOrWhiteSpace(request.Keyword))
            {
                searchRequest.Username = request.Keyword.Trim();
                searchRequest.Person = new ISearchPersons.Request
                {
                    Keyword = request.Keyword.Trim()
                };
            }

            var users = _searchUsers.Execute(searchRequest).ToList();
            var persons = _searchPersons.Respond().ToList();

            return users.Join(
                persons,
                u => u.Id,
                p => p.Id,
                (user, person) => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Role = user.Role,
                    Person = new PersonDto
                    {
                        FullName = person.FullName,
                        Email = person.Email
                    }
                }).ToList();
        }
    }
}
