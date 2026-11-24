using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IGetUserByUsername
    {
        UserDto GetByUsername(string username);
    }
}