using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetUserById : IGetUserById
    {
        private readonly IDb _db;

        public GetUserById(IDb db)
        {
            _db = db;
        }

        public User GetById(int id)
        {
            var query = _db.Query<User>()
                .Where(u => u.Id == id);

            return query.SingleOrDefault();
        }
    }
}