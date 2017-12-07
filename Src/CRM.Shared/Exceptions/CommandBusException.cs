using System;

namespace CRM.Exceptions
{
	public class CommandBusException : CrmException
	{
		public CommandBusException()
		{
		}

		public CommandBusException(string message)
			: base(message)
		{
		}

		public CommandBusException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}