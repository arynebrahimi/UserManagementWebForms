using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IDb _db;

        public UpdateUser(IDb db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            _db.Update(user);
            _db.SaveChanges();
        }
    }
}
