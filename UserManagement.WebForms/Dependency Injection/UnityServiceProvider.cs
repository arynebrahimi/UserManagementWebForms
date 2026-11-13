using System;
using Unity;

namespace UserManagement.WebForms.DependencyInjection
{
	public class UnityServiceProvider : IServiceProvider
	{
		private readonly IUnityContainer _container;

		public UnityServiceProvider(IUnityContainer container)
		{
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			return _container.Resolve(serviceType);
		}
	}
}