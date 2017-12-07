namespace CRM.Diagnostic
{
	public interface ILogger
	{
		/// <summary>System is unusable.</summary>
		/// <param name="message"></param>
		void Panic(LogMessage message);

		/// <summary>Action must be taken immediately.</summary>
		/// <param name="message"></param>
		void Alert(LogMessage message);

		/// <summary>Critical conditions.</summary>
		/// <param name="message"></param>
		void Critical(LogMessage message);

		/// <summary>Error conditions.</summary>
		/// <param name="message"></param>
		void Error(LogMessage message);

		/// <summary>Warning conditions.</summary>
		/// <param name="message"></param>
		void Warning(LogMessage message);

		/// <summary>Normal but significant condition.</summary>
		/// <param name="message"></param>
		void Notice(LogMessage message);

		/// <summary>Informational messages.</summary>
		/// <param name="message"></param>
		void Information(LogMessage message);

		/// <summary>Debug-level messages.</summary>
		/// <param name="message"></param>
		void Debug(LogMessage message);
	}
}