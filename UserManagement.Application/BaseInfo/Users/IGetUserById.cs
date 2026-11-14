using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IGetUserById
    {
        User GetById(int id);
    }
}