using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IAddUser
    {
        void Add(User user);
    }
}