using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IGetUserById
    {
        UserDto GetById(int id);
    }
}