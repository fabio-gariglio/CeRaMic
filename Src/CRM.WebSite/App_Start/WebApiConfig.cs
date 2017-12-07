
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace CRM.WebSite
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			//config.DependencyResolver = DependencyInjection.Container.Resolve<IDependencyResolver>();

			// Web API routes
			config.MapHttpAttributeRoutes();
		}
	}
}