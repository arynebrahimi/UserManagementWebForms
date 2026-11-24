using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IValidateLogin
    {
        bool Validate(LoginDto request);
    }
}