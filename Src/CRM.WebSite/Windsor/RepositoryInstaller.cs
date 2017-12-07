using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CRM.Data;

namespace CRM.WebSite.Windsor
{
	public class RepositoryInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromAssemblyNamed("CRM.Data")
			                          .BasedOn<IRepository>()
			                          .WithServiceAllInterfaces()
			                          .LifestyleSingleton());
		}
	}
}