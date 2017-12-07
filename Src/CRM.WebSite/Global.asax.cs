using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using CRM.Security;

namespace CRM.WebSite
{

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			GlobalConfiguration.Configure(WebApiConfig.Register);

			GlobalConfiguration.Configure(DependencyInjection.Config);
			DependencyInjection.Container.Register(Component.For<HttpServerUtility>().Instance(Server));

			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpActionInvoker), DependencyInjection.Container.Resolve<IHttpActionInvoker>());
			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(DependencyInjection.Container.Kernel));
		}
		
		protected void Application_Stop()
		{
			DependencyInjection.Dispose();
		}

		protected void Application_AuthenticateRequest()
		{
			var authenticationService = DependencyInjection.Container.Resolve<IAuthenticationService>();
			var contextWrapper = new HttpContextWrapper(Context);

			authenticationService.Autenticate(contextWrapper);

			DependencyInjection.Container.Release(authenticationService);
		}

	}
}
