
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces
{
    public interface IGetUserByUsername
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
