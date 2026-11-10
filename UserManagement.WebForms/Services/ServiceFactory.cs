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

        public static IGetAllUsers GetAllUsersService()
        {
            var db = new AppDbContext();
            return new GetAllUsers(db);
        }

        public static IDeleteUser GetDeleteUserService()
        {
            var db = new AppDbContext();
            return new DeleteUser(db);
        }

        public static IGetUserById GetUserByIdService()
        {
            var db = new AppDbContext();
            return new GetUserById(db);
        }

        public static IUpdateUser GetUpdateUserService()
        {
            var db = new AppDbContext();
            return new UpdateUser(db);
        }
        public static IUsernameExists GetUsernameExistsService()
        {
            var db = new AppDbContext();
            return new UsernameExists(db);
        }
    }
}