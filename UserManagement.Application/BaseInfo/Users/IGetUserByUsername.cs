using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IGetUserByUsername
    {
        User GetByUsername(string username);
    }
}