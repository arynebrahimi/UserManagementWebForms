using System.Linq;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class ValidateLogin : IValidateLogin
    {
        private readonly ISearchUsers _searchUsers;

        public ValidateLogin(ISearchUsers searchUsers)
        {
            _searchUsers = searchUsers;
        }

        public bool Validate(LoginDto request)
        {
            if (request == null)
                return false;

            string username = request.UserName.Trim();

            return _searchUsers.Execute(
                new ISearchUsers.Request
                {
                    Username = username
                })
                .Any(u => u.UserName == username &&
                          u.Password == request.Password);
        }
    }
}
