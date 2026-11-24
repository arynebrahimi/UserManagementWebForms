using System;
using System.Linq;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
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
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                throw new Exception("Please enter username and password.");
            }

            string username = request.UserName.Trim();

            bool isValid = _searchUsers.Execute(
                new ISearchUsers.Request
                {
                    Username = username
                })
                .Any(u => u.UserName == username &&
                          u.Password == request.Password);

            if (!isValid)
                throw new Exception("Invalid username or password.");

            return true;
        }
    }
}
