using System.IO.Abstractions;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CRM.WebSite.SignalR;
using Microsoft.AspNet.SignalR;

namespace CRM.WebSite.Windsor
{
	public class WebSiteInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IFileSystem>()
			                            .ImplementedBy<FileSystem>()
			                            .LifestyleSingleton(),
			                   Component.For<IHttpActionInvoker>()
			                            .ImplementedBy<MonitorApiControllerActionInvoker>()
			                            .LifestyleSingleton(),
			                   Component.For<IHubContext>()
			                            .UsingFactoryMethod(() => GlobalHost.ConnectionManager.GetHubContext<CrmHub>())
			                            .LifestyleTransient());
		}
	}
}