using System;
using System.Threading.Tasks;

namespace CRM.Diagnostic
{
	public class DefaultMonitor : IMonitor
	{
		private readonly IMonitorListener[] _listeners;

		public DefaultMonitor(IMonitorListener[] listeners)
		{
			_listeners = listeners;
		}

		public void Trace(MonitorEvent @event)
		{
			for (var i = 0; i < _listeners.Length; i++)
			{
				try
				{
					var listener = _listeners[i];
					Task.Run(() => listener.Trace(@event));
				}
				catch (Exception ex)
				{
					System.Diagnostics.Trace.TraceError(ex.ToString());
				}
			}
		}
	}
}