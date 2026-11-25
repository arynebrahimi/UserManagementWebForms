using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IAuthenticateUser
    {
        bool Authenticate(LoginDto request);
    }
}