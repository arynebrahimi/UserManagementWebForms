
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces
{
    public interface IAuthenticateUser
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
