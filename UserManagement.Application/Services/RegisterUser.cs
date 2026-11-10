using System;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
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

        public async Task<bool> RegisterAsync(User user)
        {
            user.UserName = user.UserName.Trim();

            bool exists = await _usernameExists.ExistsAsync(user.UserName);

            if (exists)
                return false;

            user.Role = "User";
            user.CreatedAt = DateTime.UtcNow;

            await _addUser.AddAsync(user);

            return true;
        }
    }
}