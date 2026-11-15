using System.Collections.Generic;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IGetAllUsers
    {
        List<User> GetAll();
    }
}
