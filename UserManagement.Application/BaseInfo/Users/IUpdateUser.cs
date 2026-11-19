using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IUpdateUser
    {
        void Update(UpdateUserDto request);
    }
}