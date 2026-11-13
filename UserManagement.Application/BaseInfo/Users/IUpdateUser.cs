
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IUpdateUser
    {
        Task UpdateAsync(User user);
    }
}
