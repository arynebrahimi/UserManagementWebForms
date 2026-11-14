using System;
using UserManagement.Domain.Entities;
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

        public bool Register(User user)
        {
            user.UserName = user.UserName.Trim();

            bool exists = _usernameExists.Exists(user.UserName);

            if (exists)
                return false;

            user.Role = "User";
            user.CreatedAt = DateTime.UtcNow;

            _addUser.Add(user);

            return true;
        }
    }
}