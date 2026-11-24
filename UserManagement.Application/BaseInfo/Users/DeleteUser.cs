using System;
using System.Linq;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.BaseInfo.Users
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IDb _db;
        private readonly ISearchUsers _searchUsers;

        public DeleteUser(IDb db, ISearchUsers searchUsers)
        {
            _db = db;
            _searchUsers = searchUsers;
        }

        public void Delete(int id)
        {
            var user = _searchUsers.Execute(
                new ISearchUsers.Request { Id = id })
                .SingleOrDefault();

            if (user == null)
                throw new Exception("User not found.");

            _db.DeleteById<User>(id);
            _db.DeleteById<Person>(user.PersonId);

            _db.SaveChanges();
        }
    }
}
