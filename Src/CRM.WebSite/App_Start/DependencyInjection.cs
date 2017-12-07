using System.Net.Http;
using System.Web.Http;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace CRM.WebSite
{
	public static class DependencyInjection
	{
		public static readonly IWindsorContainer Container = new WindsorContainer();

		public static void Config(HttpConfiguration config)
		{
			Container.Kernel.Resolver.AddSubResolver(new ArrayResolver(Container.Kernel));

			Container.Install(FromAssembly.This());

			foreach (var delegatingHandler in Container.ResolveAll<DelegatingHandler>())
			{
				config.MessageHandlers.Add(delegatingHandler);
			}

			config.DependencyResolver = new WindsorDependencyResolver(Container.Kernel);
		}

		public static void Dispose()
		{
			if (null == Container) return;

			Container.Dispose();
		}
	}
}