using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class ValidateLogin : IValidateLogin
    {
        private readonly IDb _db;

        public ValidateLogin(IDb db)
        {
            _db = db;
        }

        public bool Validate(string username, string password)
        {
            username = username.Trim();

            return _db.Query<User>()
                .Any(u => u.UserName == username &&
                          u.Password == password);
        }
    }
}