
using System.Collections.Generic;
using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly ISearchUsers _searchUsers;

        public GetAllUsers(ISearchUsers searchUsers)
        {
            _searchUsers = searchUsers;
        }

        public List<UserDto> GetAll(UserSearchDto request = null)
        {
            var searchRequest = new ISearchUsers.Request();

            if (request != null &&
                !string.IsNullOrWhiteSpace(request.Keyword))
            {
                string keyword = request.Keyword.Trim();

                searchRequest.Username = keyword;

                searchRequest.Person = new ISearchPersons.Request
                {
                    Keyword = keyword
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
                })
                .ToList();
        }
    }
}
