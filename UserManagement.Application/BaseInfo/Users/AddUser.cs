using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
{
    public class AddUser : IAddUser
    {
        private readonly IDb _db;

        public AddUser(IDb db)
        {
            _db = db;
        }

        public void Add(User user)
        {
            _db.Add(user);
            _db.SaveChanges();
        }
    }
}