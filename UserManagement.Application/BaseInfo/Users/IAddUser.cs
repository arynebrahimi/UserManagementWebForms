using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IAddUser
    {
        void Add(User user);
    }
}