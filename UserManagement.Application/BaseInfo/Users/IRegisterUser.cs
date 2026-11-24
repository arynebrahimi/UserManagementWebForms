using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IRegisterUser
    {
        bool Register(RegisterUserDto request);
    }
}