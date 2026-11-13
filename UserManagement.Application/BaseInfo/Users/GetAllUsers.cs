using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

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

        public Task<List<User>> GetAllAsync()
        {
            var query = _searchUsers.Respond();

            return _db.ToListAsync(query);
        }
    }
}