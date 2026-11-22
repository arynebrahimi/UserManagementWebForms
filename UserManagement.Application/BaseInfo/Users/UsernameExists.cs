using System.Linq;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.Application.BaseInfo.Users.Services
{
    public class UsernameExists : IUsernameExists
    {
        private readonly ISearchUsers _searchUsers;

        public UsernameExists(ISearchUsers searchUsers)
        {
            _searchUsers = searchUsers;
        }

        public bool Exists(string username, int id = 0)
        {
            return _searchUsers.Execute(
                new ISearchUsers.Request
                {
                    Username = username
                })
                .Any(u => u.UserName == username && u.Id != id);
        }
    }
}
