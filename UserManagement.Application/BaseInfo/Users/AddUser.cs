using System;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class AddUser : IAddUser
    {
        private readonly IDb _db;
        private readonly IUsernameExists _usernameExists;

        public AddUser(
            IDb db,
            IUsernameExists usernameExists)
        {
            _db = db;
            _usernameExists = usernameExists;
        }

        public bool Add(RegisterUserDto request)
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

            _db.Add(user);
            _db.SaveChanges();

            return true;
        }
    }
}
