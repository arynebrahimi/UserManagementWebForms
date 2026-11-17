using System.Collections.Generic;
using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly ISearchUsers _searchUsers;

        public GetAllUsers(ISearchUsers searchUsers)
        {
            _searchUsers = searchUsers;
        }
        public List<User> GetAll()
        {
            return _searchUsers.Execute().ToList();
        }
    }
}