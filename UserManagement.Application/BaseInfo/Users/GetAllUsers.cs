using System.Collections.Generic;
using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly ISearchUsers _searchUsers;
        private readonly IDb _db;

        public GetAllUsers(ISearchUsers searchUsers, IDb db)
        {
            _searchUsers = searchUsers;
            _db = db;
        }

        public List<User> GetAll()
        {
            var users = _searchUsers.Respond().ToList();
            var persons = _db.Query<Person>().ToList();

            foreach (var user in users)
            {
                foreach (var person in persons)
                {
                    if (user.Id == person.Id)
                    {
                        user.Person = person;
                        break;
                    }
                }
            }

            return users;
        }
    }
}