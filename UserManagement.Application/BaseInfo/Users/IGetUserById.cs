
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IGetUserById
    {
        Task<User> GetByIdAsync(int Id);
    }
}
