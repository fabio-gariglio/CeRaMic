using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CRM.EventSourcing;

namespace CRM.WebSite.Windsor
{
	public class DomainInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			InstallByAssemblyName(container, "CRM.Domain");
			InstallByAssemblyName(container, "CRM.WebSite");
		}

		private static void InstallByAssemblyName(IWindsorContainer container, string assemblyName)
		{
			DomainRegistrator.RegisterCommands(container, Classes.FromAssemblyNamed(assemblyName));
			DomainRegistrator.RegisterEvents(container, Classes.FromAssemblyNamed(assemblyName));
		}
	}
}