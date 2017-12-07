using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using CRM.Diagnostic;
using CRM.Security;

namespace CRM.WebSite
{
	public class MonitorApiControllerActionInvoker : ApiControllerActionInvoker
	{
		private readonly IMonitor _monitor;
		private readonly ILogger _logger;

		public MonitorApiControllerActionInvoker(IMonitor monitor, ILogger logger)
		{
			_monitor = monitor;
			_logger = logger;
		}



		public override Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext,
		                                                            CancellationToken cancellationToken)
		{
			var result = _monitor.TraceHttpRequest(() => base.InvokeActionAsync(actionContext, cancellationToken));

			if (result.Exception == null) return result;

			var exception = result.Exception;
			var controllerName = actionContext.ControllerContext.Controller.GetType().Name;
			var logMessage = new LogMessage(exception.InnerExceptions.First().Message, exception.ToString())
				.OnController(controllerName)
				.ByUser();

			_logger.Error(logMessage);

			return result;
		}
	}
}