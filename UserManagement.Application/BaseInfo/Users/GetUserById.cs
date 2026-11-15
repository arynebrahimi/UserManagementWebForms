using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetUserById : IGetUserById
    {
        private readonly IDb _db;

        public GetUserById(IDb db)
        {
            _db = db;
        }

        public User GetById(int id)
        {
            var users = _db.Query<User>();

            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    var persons = _db.Query<Person>();

                    foreach (var person in persons)
                    {
                        if (person.Id == user.Id)
                        {
                            user.Person = person;
                            break;
                        }
                    }

                    return user;
                }
            }

            return null;
        }
    }
}