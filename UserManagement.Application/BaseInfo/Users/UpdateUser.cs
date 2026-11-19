using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IDb _db;

        public UpdateUser(IDb db)
        {
            _db = db;
        }

        public void Update(UpdateUserDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email))
                return;

            if (request == null)
                return;

            var user = _db.Query<User>()
                .SingleOrDefault(u => u.Id == request.Id);

            var person = _db.Query<Person>()
                .SingleOrDefault(p => p.Id == request.Id);

            if (user == null || person == null)
                return;

            user.UserName = request.UserName.Trim();
            person.FullName = request.FullName.Trim();
            person.Email = request.Email.Trim();

            _db.Update(user);
            _db.Update(person);

            _db.SaveChanges();
        }
    }
}