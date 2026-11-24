using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IUpdateUser
    {
        bool Update(UpdateUserDto request);
    }
}