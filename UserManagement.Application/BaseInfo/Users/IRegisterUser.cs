
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IRegisterUser
    {
        Task<bool> RegisterAsync(User user);
    }
}
