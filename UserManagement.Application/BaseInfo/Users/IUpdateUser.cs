using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IUpdateUser
    {
        bool Update(UpdateUserDto request);
    }
}