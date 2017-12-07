using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using CRM.Diagnostic;
using CRM.Security;

namespace CRM.WebSite
{
	public static class MonitorExtensions
	{
		public static async Task<T> TraceHttpRequest<T>(this IMonitor monitor, Func<Task<T>> action)
		{
			var monitorEvent = CreateMonitorEvent();

			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var result = await action();

			stopWatch.Stop();
			monitorEvent.Milliseconds = stopWatch.ElapsedMilliseconds;

			monitor.Trace(monitorEvent);

			return result;
		}

		private static MonitorEvent CreateMonitorEvent()
		{
			var context = HttpContext.Current;
			var data = new RequestMonitorData(context);

			var monitorEvent = new MonitorEvent(context.Request.RawUrl, data);
			return monitorEvent;
		}

		private class RequestMonitorData
		{
			public Guid UserId { get; private set; }
			public string ClientType { get; private set; }

			public RequestMonitorData(HttpContext context)
			{
				var user = context.User as CrmUser;

				if (null != user) UserId = user.Id;

				ClientType = context.Request.Browser.Browser;
			}


		}
	}
}