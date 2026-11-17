using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistence.DataAccess.Persons
{
    public class SearchPersons : ISearchPersons
    {
        private readonly IDb _db;
        public SearchPersons(IDb db)
        {
            _db = db;
        }
        public IQueryable<Person> Respond(ISearchPersons.Request request = null)
        {
            var items = _db.Query<Person>();

            if (request == null)
                return items;

            if (request.Id.HasValue)
            items = items.Where(p => p.Id == request.Id);

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                string keyword = request.Keyword.Trim();

                items = items.Where(p => p.FullName.Contains(keyword)
                || p.Email.Contains(keyword));
            }
            return items;
        }

    }
}
