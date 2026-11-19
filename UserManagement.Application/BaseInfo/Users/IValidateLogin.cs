using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IValidateLogin
    {
        bool Validate(LoginDto request);
    }
}