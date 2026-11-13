using System.Threading.Tasks;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IValidateLogin
    {
        Task<bool> ValidateAsync(string username, string password);
    }
}
