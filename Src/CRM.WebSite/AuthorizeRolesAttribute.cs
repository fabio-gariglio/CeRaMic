using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CRM.Security;

namespace CRM.WebSite
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class AuthorizeRolesAttribute : FilterAttribute, IAuthorizationFilter
	{
		private const StringComparison CompareMode = StringComparison.InvariantCultureIgnoreCase;

		private readonly string[] _roles;

		public AuthorizeRolesAttribute(params string[] roles)
		{
			_roles = roles;
		}

		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
		                                            CancellationToken cancellationToken,
		                                            Func<Task<HttpResponseMessage>> continuation)
		{
			var user = actionContext.RequestContext.Principal as CrmUser;

			if (IsInRole(user)) return continuation();

			return Task.Run(() => GetUnauthorizedMessage(), cancellationToken);
		}

		private bool IsInRole(CrmUser user)
		{
			return null != user && _roles.Any(r => string.Equals(r, user.Role, CompareMode));
		}

		private static HttpResponseMessage GetUnauthorizedMessage()
		{
			return new HttpResponseMessage(HttpStatusCode.Unauthorized)
			       {
				       Content = new StringContent("Missing permissions")
			       };
		}
	}
}