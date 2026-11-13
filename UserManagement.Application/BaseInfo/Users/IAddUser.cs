using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IAddUser
    {
        Task AddAsync(User user);
    }
}