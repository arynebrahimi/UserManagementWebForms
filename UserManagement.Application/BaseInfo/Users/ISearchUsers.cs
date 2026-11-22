using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.BaseInfo.Persons.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface ISearchUsers
    {
        IQueryable<User> Execute(Request request = null);

        class Request
        {
            public int? Id { get; set; }
            public string Username { get; set; }
            public ISearchPersons.Request Person { get; set; }
        }
    }
}