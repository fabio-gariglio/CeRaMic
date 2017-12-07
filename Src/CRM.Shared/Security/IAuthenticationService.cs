using System.Web;

namespace CRM.Security
{
	public interface IAuthenticationService
	{
		void Autenticate(HttpContextBase context);
		void Login(HttpContextBase context, UserCredentials credentials);
		void Logout(HttpContextBase context);
	}
}
