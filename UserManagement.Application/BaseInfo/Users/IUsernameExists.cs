namespace UserManagement.Application.BaseInfo.Users
{
    public interface IUsernameExists
    {
        bool Exists(string username, int id = 0);
    }
}