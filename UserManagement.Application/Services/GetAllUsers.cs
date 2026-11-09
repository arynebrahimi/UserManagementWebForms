
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly IDb _db;

        public GetAllUsers(IDb db)
        {
            _db = db;
        }

        public Task<List<User>> GetAllAsync()
        {
            var query = _db.Query<User>()
                .OrderBy(u => u.ID);

            return _db.ToListAsync(query);
        }
    }
}
