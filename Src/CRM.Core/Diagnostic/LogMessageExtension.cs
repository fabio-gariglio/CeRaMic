using System.Web;
using CRM.Security;

namespace CRM.Diagnostic
{
	public static class LogMessageExtension
	{
		public static LogMessage OnController(this LogMessage message, string controllerName)
		{
			message.Properties["ControllerName"] = controllerName;

			return message;
		}

		public static LogMessage ByUser(this LogMessage message)
		{
			var user = HttpContext.Current.User as CrmUser;

			if (null == user) return message;

			message.Properties["UserId"] = user.Id;

			return message;
		}
	}
}
