using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
{
    public class GetUserByUsername : IGetUserByUsername
    {
        private readonly ISearchUsers _searchUsers;

        public GetUserByUsername(ISearchUsers searchUsers )
        {
            _searchUsers = searchUsers;
        }

        public User GetByUsername(string username)
        {
            return _searchUsers.Execute(
                new ISearchUsers.Request
                {
                    Username = username
                })
                .SingleOrDefault();

            
        }
    }
}
