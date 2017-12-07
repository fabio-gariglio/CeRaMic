using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace CRM.WebSite
{
	public class WindsorDependencyResolver : IDependencyResolver
	{
		private readonly IKernel _kernel;

		public WindsorDependencyResolver(IKernel kernel)
		{
			_kernel = kernel;
		}

		public void Dispose()
		{
		}

		public object GetService(Type serviceType)
		{
			return _kernel.HasComponent(serviceType)
				? _kernel.Resolve(serviceType)
				: null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.HasComponent(serviceType)
				? (IEnumerable<object>) _kernel.ResolveAll(serviceType)
				: Enumerable.Empty<object>();
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}
	}
}