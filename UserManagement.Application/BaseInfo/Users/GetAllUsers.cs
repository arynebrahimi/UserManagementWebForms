using System.Collections.Generic;
using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
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

            return _searchUsers.Execute(searchRequest)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Role = u.Role,
                    Person = new PersonDto
                    {
                        FullName = u.Person.FullName,
                        Email = u.Person.Email
                    }
                }).ToList();

        }
    }
}
