
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Persistence.Data;

namespace UserManagement.WebForms.Services
{
    public class ServiceFactory
    {
        public static IRegisterUser GetRegisterService()
        {
            var db = new AppDbContext();

            return new RegisterUser(
                new AddUser(db),
                new UsernameExists(db)
            );
        }

        public static IAuthenticateUser GetLoginService()
        {
            var db = new AppDbContext();

            return new AuthenticateUser(
                new GetUserByUsername(db)
            );
        }
    }
}
