using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.BaseInfo.Users;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Users.Services;
using UserManagement.Application.Interfaces;
using UserManagement.Persistence.BaseInfos.Persons;
using UserManagement.Persistence.BaseInfos.Users;
using UserManagement.Persistence.Data;
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
            container.RegisterType<IAuthenticateUser, AuthenticateUser>();

            HttpRuntime.WebObjectActivator =
                new UnityServiceProvider(container);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}