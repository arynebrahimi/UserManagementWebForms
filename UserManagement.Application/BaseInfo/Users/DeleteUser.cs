using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IDb _db;

        public DeleteUser(IDb db)
        {
            _db = db;
        }

        public void Delete(int id)
        {
            bool deleted = _db.DeleteById<User>(id);

            if (deleted)
            {
                _db.SaveChanges();
            }
        }
    }
}