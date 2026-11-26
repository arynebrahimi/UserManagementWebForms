using System.Collections.Generic;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.BaseInfo.Users
{
    public interface IGetAllUsers
    {
        List<UserDto> GetAll(UserSearchDto request = null);
        int GetCount(UserSearchDto request = null);
    }
}