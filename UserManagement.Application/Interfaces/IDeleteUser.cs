
using System.Threading.Tasks;

namespace UserManagement.Application.Interfaces
{
    public interface IDeleteUser
    {
        Task DeleteAsync(int id);
    }
}
