using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CRM.Diagnostic;

namespace CRM.WebSite.Windsor
{
	public class MonitorInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(WindsorExtensions.FromBin().BasedOn<IMonitorListener>().WithServiceAllInterfaces().LifestyleSingleton(),
			                   Component.For<IMonitor>().ImplementedBy<DefaultMonitor>().LifestyleSingleton());
		}
	}
}