using System.Linq;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
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