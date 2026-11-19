using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IRegisterUser
    {
        bool Register(RegisterUserDto request);
    }
}