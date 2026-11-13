using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Application.BaseInfo.Users.Services;
using UserManagement.Application.Interfaces;
using UserManagement.Persistence.Data;
using UserManagement.WebForms.DependencyInjection;
using UserManagement.Application.BaseInfo.Users;

namespace UserManagement.WebForms
{
	public class Global : HttpApplication
	{
		void Application_Start(object sender, EventArgs e)
		{
			var container = new UnityContainer();

			container.RegisterType<IDb, AppDbContext>();
			container.RegisterType<ISearchUsers, SearchUsers>();
			container.RegisterType<IGetAllUsers, GetAllUsers>();
			container.RegisterType<IGetUserById, GetUserById>();
			container.RegisterType<IUsernameExists, UsernameExists>();
			container.RegisterType<IUpdateUser, UpdateUser>();
			container.RegisterType<IValidateLogin, ValidateLogin>();
			container.RegisterType<IRegisterUser, RegisterUser>();
			container.RegisterType<IGetAllUsers, GetAllUsers>();

			HttpRuntime.WebObjectActivator =
				new UnityServiceProvider(container);

			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}