namespace CRM.Diagnostic
{
	public enum LogSeverity
	{
		Panic = 0,				// System is unusable.
		Alert = 1,				// Action must be taken immediately.
		Critical = 2,			// Critical conditions.
		Error = 3,				// Error conditions.
		Warning = 4,			// Warning conditions.
		Notice = 5,				// Normal but significant condition.
		Information = 6,	// Informational messages.
		Debug = 7					// Debug-level messages.
	}
}
