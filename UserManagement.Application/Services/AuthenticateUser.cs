
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class AuthenticateUser : IAuthenticateUser
    {
        private readonly IGetUserByUsername _getUserByUsername;

        public AuthenticateUser(IGetUserByUsername getUserByUsername)
        {
            _getUserByUsername = getUserByUsername;
        }

        public async Task<User> AuthenticateAsync(
            string username,
            string password)
        {
            var user = await _getUserByUsername.GetByUsernameAsync(username);

            if (user == null)
                return null;

            if (user.Password != password)
                return null;

            return user;
        }
    }
}
