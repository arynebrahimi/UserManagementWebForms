using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IRegisterUser
    {
        bool Register(User user);
    }
}