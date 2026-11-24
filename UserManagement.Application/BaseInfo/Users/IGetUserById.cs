using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IGetUserById
    {
        UserDto GetById(int id);
    }
}