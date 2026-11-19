using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IGetUserByUsername
    {
        UserDto GetByUsername(string username);
    }
}