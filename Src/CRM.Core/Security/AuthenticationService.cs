using System;
using System.Net;
using System.Web;
using CRM.Configuration;
using CRM.Data;
using CRM.Users;

namespace CRM.Security
{
	public class AuthenticationService : IAuthenticationService, ISingleton
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfigurationProvider _configurationProvider;
		private const string CookieName = "CRM";
		private readonly Lazy<Guid> _administratorUserId;
		private readonly Lazy<string> _administratorEmail;
		private readonly Lazy<string> _administratorPassword;
		private readonly Lazy<CrmUser> _administratorUser;

		public AuthenticationService(IUserRepository userRepository, IConfigurationProvider configurationProvider)
		{
			_userRepository = userRepository;
			_configurationProvider = configurationProvider;

			_administratorUserId = new Lazy<Guid>(() => _configurationProvider.GetApplicationSetting<Guid>("administratorUserId"));
			_administratorEmail = new Lazy<string>(() => _configurationProvider.GetApplicationSetting<string>("administratorEmail"));
			_administratorPassword = new Lazy<string>(() => _configurationProvider.GetApplicationSetting<string>("administratorPassword"));
			_administratorUser = new Lazy<CrmUser>(AdministratorUserFactory);
		}

		public void Autenticate(HttpContextBase context)
		{
			var cookie = GetAuthenticationCookie(context);

			if (null == cookie) return;

			Guid userId;

			if (!Guid.TryParse(cookie.Value, out userId)) return;

			var user = LoadUser(userId);

			context.User = user;
		}

		public void Login(HttpContextBase context, UserCredentials credentials)
		{
			if (null == credentials) return;
			if (string.IsNullOrWhiteSpace(credentials.Email)) return;
			if (string.IsNullOrWhiteSpace(credentials.Password)) return;

			var user = LoadUser(credentials);

			if (null == user) return;

			var cookie = new HttpCookie(CookieName, user.Id.ToString())
			             {
				             Expires = credentials.RememberMe
					             ? DateTime.Now.AddDays(15)
					             : DateTime.MinValue
			             };

			context.Response.Cookies.Add(cookie);
			context.Response.RedirectLocation = "/";
			context.Response.StatusCode = (int)HttpStatusCode.Redirect;

			context.User = user;
		}

		public void Logout(HttpContextBase context)
		{
			var cookie = GetAuthenticationCookie(context);

			if (null == cookie) return;

			cookie.Expires = DateTime.UtcNow;

			context.Response.Cookies.Add(cookie);
			context.User = null;
		}

		private CrmUser LoadUser(UserCredentials credentials)
		{
			if (IsAdministratorCredentials(credentials))
			{
				return _administratorUser.Value;
			}

			var userContract = _userRepository.GetByCredentials(credentials);

			return LoadUser(userContract);
		}

		private bool IsAdministratorCredentials(UserCredentials credentials)
		{
			return credentials.Email == _administratorEmail.Value
			       && credentials.Password == _administratorPassword.Value;
		}

		private CrmUser LoadUser(Guid id)
		{
			if (id == _administratorUserId.Value)
			{
				return _administratorUser.Value;
			}

			var userContract = _userRepository.GetById(id);

			return LoadUser(userContract);
		}

		private static CrmUser LoadUser(UserContract userContract)
		{
			if (null == userContract) return null;

			return new CrmUser(userContract.Id, userContract.Name, userContract.Role);
		}

		private static HttpCookie GetAuthenticationCookie(HttpContextBase context)
		{
			var cookie = context.Request.Cookies[CookieName];

			return cookie;
		}

		private CrmUser AdministratorUserFactory()
		{
			var administratorId = Guid.Parse(_configurationProvider.GetApplicationSetting<string>("administratorUserId"));
			var administratorUsername = _configurationProvider.GetApplicationSetting<string>("administratorUsername");

			return new CrmUser(administratorId, administratorUsername, UserRoles.Administrator);
		}
	}
}
