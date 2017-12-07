using System.Web.Mvc;
using CRM.Security;

namespace CRM.WebSite.Controllers
{
	public class UserController : Controller
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly IPasswordRecoveryService _passwordRecoveryService;

		public UserController(IAuthenticationService authenticationService,
		                      IPasswordRecoveryService passwordRecoveryService)
		{
			_authenticationService = authenticationService;
			_passwordRecoveryService = passwordRecoveryService;
		}

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(UserCredentials credentials)
		{
			_authenticationService.Login(HttpContext, credentials);

			return View();
		}

		[HttpGet]
		public ActionResult Recovery()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Recovery(string email)
		{
			_passwordRecoveryService.Recovery(email);

			return View();
		}

		public ActionResult Logout()
		{
			_authenticationService.Logout(HttpContext);

			return RedirectToAction("Login", "User");
		}
	}
}