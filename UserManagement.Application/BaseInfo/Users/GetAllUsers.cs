using System.Collections.Generic;
using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Persons.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly ISearchUsers _searchUsers;
        private readonly IDb _db;

        public GetAllUsers(ISearchUsers searchUsers, IDb db)
        {
            _searchUsers = searchUsers;
            _db = db;
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

            // Execute each query separately
            var users = _searchUsers.Execute(searchRequest).ToList();
            var persons = _db.Query<Person>().ToList();

            // Map entities to DTOs in memory
            return users.Select(user =>
            {
                var person = persons.FirstOrDefault(
                    p => p.Id == user.Id);

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
            }).ToList();
        }
    }
}