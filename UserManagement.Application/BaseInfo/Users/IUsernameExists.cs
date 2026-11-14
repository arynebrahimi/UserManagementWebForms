namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IUsernameExists
    {
        bool Exists(string username, int? exceptUserId = null);
    }
}