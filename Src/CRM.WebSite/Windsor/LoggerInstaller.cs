using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CRM.Diagnostic;

namespace CRM.WebSite.Windsor
{
	public class LoggerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(WindsorExtensions.FromBin().BasedOn<ILoggerListener>().WithServiceAllInterfaces().LifestyleSingleton(),
			                   Component.For<ILogger>().ImplementedBy<DefaultLogger>().LifestyleSingleton());
		}
	}
}