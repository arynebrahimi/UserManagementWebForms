using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class GetUserByUsername : IGetUserByUsername
    {
        private readonly IDb _db;

        public GetUserByUsername(IDb db)
        {
            _db = db;
        }

        public UserDto GetByUsername(string username)
        {
            var user = _db.Query<User>()
                .SingleOrDefault(u => u.UserName == username);

            if (user == null)
                return null;

            var person = _db.Query<Person>()
                .SingleOrDefault(p => p.Id == user.Id);

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
                Person = person == null ? null : new PersonDto
                {
                    FullName = person.FullName,
                    Email = person.Email
                }
            };
        }
    }
}