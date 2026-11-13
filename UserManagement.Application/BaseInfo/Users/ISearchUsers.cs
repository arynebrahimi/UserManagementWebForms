using System.Linq;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface ISearchUsers
    {
        IQueryable<User> Respond(Request request = null);

        class Request
        {
            public string Username { get; set; }
        }
    }
}