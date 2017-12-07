using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CRM.WebSite.Windsor
{
	public class ComponentsInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(WindsorExtensions.FromBin().BasedOn<ITransient>().WithServiceAllInterfaces().LifestyleTransient(),
			                   WindsorExtensions.FromBin().BasedOn<ISingleton>().WithServiceAllInterfaces().LifestyleSingleton());
		}
	}
}