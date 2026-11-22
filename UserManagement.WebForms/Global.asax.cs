
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
    public class Global : HttpApplication, IContainerProvider
    {
        private static IUnityContainer _container;

        void Application_Start(object sender, EventArgs e)
        {
            _container = new UnityContainer();

            _container.RegisterType<IDb, AppDbContext>();

            _container.RegisterType<ISearchUsers, SearchUsers>();
            _container.RegisterType<ISearchPersons, SearchPersons>();
            _container.RegisterType<IGetAllUsers, GetAllUsers>();
            _container.RegisterType<IGetUserById, GetUserById>();
            _container.RegisterType<IUsernameExists, UsernameExists>();
            _container.RegisterType<IAddUser, AddUser>();
            _container.RegisterType<IUpdateUser, UpdateUser>();
            _container.RegisterType<IDeleteUser, DeleteUser>();
            _container.RegisterType<IValidateLogin, ValidateLogin>();
            _container.RegisterType<IRegisterUser, RegisterUser>();

            HttpRuntime.WebObjectActivator = this;

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}
