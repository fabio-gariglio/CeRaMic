﻿using System.Web.Mvc;
using System.Web.Routing;

namespace CRM.WebSite
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});

			routes.MapMvcAttributeRoutes();

			routes.MapRoute(name: "Default",
			                url: "{controller}/{action}/{id}",
			                defaults: new
			                          {
				                          controller = "Home",
				                          action = "Index",
				                          id = UrlParameter.Optional
			                          });
		}
	}
}