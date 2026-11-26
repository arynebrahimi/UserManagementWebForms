using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IAddUser
    {
        bool Add(RegisterUserDto request, out User user);
    }
}
