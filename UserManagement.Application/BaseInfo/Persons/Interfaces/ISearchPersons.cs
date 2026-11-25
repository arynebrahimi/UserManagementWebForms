using System.Linq;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Persons.Interfaces
{
    public interface ISearchPersons
    {
        IQueryable<Person> Respond(Request request = null);

        class Request
        {
            public int? Id { get; set; }
            public string Keyword { get; set; }
        }
    }
}