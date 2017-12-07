namespace CRM.Diagnostic
{
	public interface IMonitorListener
	{
		void Trace(MonitorEvent @event);
	}
}
