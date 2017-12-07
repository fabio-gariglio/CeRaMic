using System;
using System.Collections.Generic;

namespace CRM.Diagnostic
{
	public sealed class LogMessage
	{
		public string MachineName { get; private set; }
		public DateTime UtcTimestamp { get; private set; }
		public string Description { get; private set; }
		public string Content { get; set; }
		public IDictionary<string, object> Properties { get; private set; }

		public LogMessage(string description, string content = null)
		{
			MachineName = Environment.MachineName;
			UtcTimestamp = DateTime.UtcNow;

			Description = description;
			Content = content;
			Properties = new Dictionary<string, object>();
		}
	}
}
