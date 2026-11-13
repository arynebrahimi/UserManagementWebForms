using System.Threading.Tasks;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IUsernameExists
    {
        Task<bool> ExistsAsync(string username, int? exceptUserId = null);
    }
}