using System.Threading.Tasks;

namespace UserManagement.Application.Interfaces
{
    public interface IUsernameExists
    {
        Task<bool> ExistsAsync(string username, int? exceptUserId = null);
    }
}