
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IGetUserByUsername
    {
        User GetByUsername(string username);
    }
}
