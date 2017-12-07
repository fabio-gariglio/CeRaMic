using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace CRM.EventSourcing
{
	public static class DomainRegistrator
	{
		public static void RegisterEvents(IWindsorContainer container, FromAssemblyDescriptor assemblyDescriptor)
		{
			container.Register(assemblyDescriptor.BasedOn<IDomainEventHandler>()
			                                     .WithServiceAllInterfaces()
			                                     .LifestyleSingleton());
		}

		public static void RegisterCommands(IWindsorContainer container, FromAssemblyDescriptor assemblyDescriptor)
		{
			container.Register(assemblyDescriptor.BasedOn<IDomainCommandHandler>()
																					 .WithServiceAllInterfaces()
																					 .LifestyleSingleton());
		}
	}
}
