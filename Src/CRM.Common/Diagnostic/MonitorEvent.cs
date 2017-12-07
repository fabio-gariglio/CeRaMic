using System;

namespace CRM.Diagnostic
{
	public class MonitorEvent
	{
		public string Source { get; private set; }
		public object Data { get; private set; }
		public DateTime UtcTimestamp { get; private set; }
		public long Milliseconds { get; set; }

		public MonitorEvent(string source, object data = null)
		{
			Source = source;
			Data = data;
			UtcTimestamp = DateTime.UtcNow;
		}
	}
}
