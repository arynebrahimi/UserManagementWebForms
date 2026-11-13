using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class ValidateLogin : IValidateLogin
    {
        private readonly IDb _db;

        public ValidateLogin(IDb db)
        {
            _db = db;
        }

        public async Task<bool> ValidateAsync(
            string username, string password)
        {
            username = username.Trim();

            var query = _db.Query<User>()
                .Where(u => u.UserName == username);

            var user = await _db.FirstOrDefaultAsync(query);

            return user != null && user.Password == password;
        }
    }
}