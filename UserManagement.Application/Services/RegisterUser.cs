using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IAddUser _addUser;
        private readonly IUsernameExists _usernameExists;

        public RegisterUser(IAddUser addUser, IUsernameExists usernameExists)
        {
            _addUser = addUser;
            _usernameExists = usernameExists;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            if (await _usernameExists.ExistsAsync(user.UserName))
                return false;

            user.Role = "User";
            await _addUser.AddAsync(user);

            return true;
        }
    }
}