using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CRM.Common;
using CRM.Security;

namespace CRM.WebSite.Controllers
{
	public class HomeModel
	{
		public IEnumerable<string> Scripts { get; set; }

		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string UserRole { get; set; }
	}

	public class HomeController : Controller
	{
		private readonly IScriptsProvider _scriptsProvider;

		public HomeController(IScriptsProvider scriptsProvider)
		{
			_scriptsProvider = scriptsProvider;
		}

		public ActionResult Index()
		{
			var user = (CrmUser) HttpContext.User;

			var model = new HomeModel
			            {
				            Scripts = _scriptsProvider.GetScripts(Server.MapPath("/")),
										UserId = user.Id,
				            UserName = user.Identity.Name,
				            UserRole = user.Role
			            };

			return View(model);
		}
	}
}