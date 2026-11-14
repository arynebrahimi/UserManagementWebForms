namespace UserManagement.Application.BaseInfo.Users.Interfaces
{
    public interface IValidateLogin
    {
        bool Validate(string username, string password);
    }
}