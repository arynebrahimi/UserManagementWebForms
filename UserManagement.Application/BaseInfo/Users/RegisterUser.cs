using System;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IAddUser _addUser;
        private readonly IUsernameExists _usernameExists;

        public RegisterUser(
            IAddUser addUser,
            IUsernameExists usernameExists)
        {
            _addUser = addUser;
            _usernameExists = usernameExists;
        }

        public bool Register(RegisterUserDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                throw new Exception("Please fill in all fields.");
            }

            string username = request.UserName.Trim();

            if (_usernameExists.Exists(username))
                throw new Exception("Username already exists.");

            var user = new User
            {
                UserName = username,
                Password = request.Password,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                Person = new Person
                {
                    FullName = request.FullName.Trim(),
                    Email = request.Email.Trim()
                }
            };

            _addUser.Add(user);

            return true;
        }
    }
}
