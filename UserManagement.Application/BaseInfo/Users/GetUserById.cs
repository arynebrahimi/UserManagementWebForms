using System.Linq;
using System.Threading.Tasks;
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

        public Task<User> GetByIdAsync(int Id)
        {
            var query = _db.Query<User>()
                .Where(u => u.Id == Id);

            return _db.FirstOrDefaultAsync(query);
        }
    }
}
