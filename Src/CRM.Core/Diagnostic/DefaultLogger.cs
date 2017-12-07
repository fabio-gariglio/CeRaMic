using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CRM.Diagnostic
{
	public class DefaultLogger : ILogger
	{
		private readonly ILoggerListener[] _listeners;

		public DefaultLogger(ILoggerListener[] listeners)
		{
			_listeners = listeners;
		}

		public void Panic(LogMessage message)
		{
			Log(listener => listener.Panic, message);
		}

		public void Alert(LogMessage message)
		{
			Log(listener => listener.Alert, message);
		}

		public void Critical(LogMessage message)
		{
			Log(listener => listener.Critical, message);
		}

		public void Error(LogMessage message)
		{
			Log(listener => listener.Error, message);
		}

		public void Warning(LogMessage message)
		{
			Log(listener => listener.Warning, message);
		}
		
		public void Notice(LogMessage message)
		{
			Log(listener => listener.Notice, message);
		}

		public void Information(LogMessage message)
		{
			Log(listener => listener.Information, message);
		}

		public void Debug(LogMessage message)
		{
			Log(listener => listener.Debug, message);
		}

		private void Log(Func<ILoggerListener, Action<LogMessage>> actionSelector, LogMessage message)
		{
			for (var i = 0; i < _listeners.Length; i++)
			{
				try
				{
					var listener = _listeners[i];
					Task.Run(() => actionSelector(listener)(message));
				}
				catch (Exception ex)
				{
					Trace.TraceError(ex.ToString());
				}
			}
		}
	}
}
