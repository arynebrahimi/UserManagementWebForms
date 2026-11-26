using System;
using System.Linq;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class AuthenticateUser : IAuthenticateUser
    {
        private readonly ISearchUsers _searchUsers;

        public AuthenticateUser(ISearchUsers searchUsers)
        {
            _searchUsers = searchUsers;
        }

        public bool Authenticate(LoginDto request, out string authUsername)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                throw new Exception("Please enter username and password.");
            }

            var username = request.UserName.Trim();

            bool isValid = _searchUsers.Execute(
                new ISearchUsers.Request
                {
                    Username = username
                })
                .Any(u => u.UserName == username &&
                          u.Password == request.Password);

            if (!isValid)
                throw new Exception("Invalid username or password.");

            authUsername = username;
            return true;
        }
    }
}
