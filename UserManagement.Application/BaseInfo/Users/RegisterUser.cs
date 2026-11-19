using System;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
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
                return false;

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email))
                return false;

            string username = request.UserName.Trim();

            if (_usernameExists.Exists(username))
                return false;

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