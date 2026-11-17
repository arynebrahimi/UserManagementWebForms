using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Users.Services;
using UserManagement.Application.Interfaces;
using UserManagement.Persistence.Data;
using UserManagement.Persistence.DataAccess.Persons;
using UserManagement.Persistence.DataAccess.Users;
using UserManagement.WebForms.DependencyInjection;

namespace UserManagement.WebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var container = new UnityContainer();

            container.RegisterType<IDb, AppDbContext>();

            container.RegisterType<ISearchUsers, SearchUsers>();
            container.RegisterType<ISearchPersons, SearchPersons>();
            container.RegisterType<IGetAllUsers, GetAllUsers>();
            container.RegisterType<IGetUserById, GetUserById>();
            container.RegisterType<IUsernameExists, UsernameExists>();
            container.RegisterType<IAddUser, AddUser>();
            container.RegisterType<IUpdateUser, UpdateUser>();
            container.RegisterType<IDeleteUser, DeleteUser>();
            container.RegisterType<IValidateLogin, ValidateLogin>();
            container.RegisterType<IRegisterUser, RegisterUser>();

            HttpRuntime.WebObjectActivator = new UnityServiceProvider(container);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}