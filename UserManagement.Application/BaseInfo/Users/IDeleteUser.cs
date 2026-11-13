
using System.Threading.Tasks;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IdeleteUser
    {
        Task DeleteAsync(int Id);
    }
}
