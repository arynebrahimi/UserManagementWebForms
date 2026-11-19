using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
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

        public bool Validate(LoginDto request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
                return false;

            string username = request.UserName.Trim();

            return _db.Query<User>().Any(u =>
                u.UserName == username &&
                u.Password == request.Password);
        }
    }
}