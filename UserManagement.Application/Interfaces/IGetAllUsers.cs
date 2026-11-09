
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces
{
    public interface IGetAllUsers
    {
        Task<List<User>> GetAllAsync();
    }
}
