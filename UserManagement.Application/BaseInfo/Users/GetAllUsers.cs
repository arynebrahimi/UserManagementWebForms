using System.Collections.Generic;
using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
{
    public class GetAllUsers : IGetAllUsers
    {
        private const int PageSize = 10;

        private readonly ISearchUsers _searchUsers;

        public GetAllUsers(ISearchUsers searchUsers)
        {
            _searchUsers = searchUsers;
        }

        private IQueryable<User> Search(UserSearchDto request)
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

            return _searchUsers.Execute(searchRequest);
        }

        public List<UserDto> GetAll(UserSearchDto request = null)
        {
            int pageNumber = request == null || request.PageNumber < 1
                ? 1
                : request.PageNumber;

            return Search(request)
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
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

        public int GetCount(UserSearchDto request = null)
        {
            return Search(request).Count();
        }
    }
}