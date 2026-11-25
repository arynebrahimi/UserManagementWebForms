using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IAddUser
    {
        bool Add(RegisterUserDto request);
    }
}
