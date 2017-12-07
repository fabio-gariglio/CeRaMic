using System;

namespace CRM.Diagnostic
{
	public interface IMonitor
	{
		void Trace(MonitorEvent @event);
	}
}
