using System;

namespace CRM.Exceptions
{
	public class CrmException : Exception
	{
		public CrmException()
		{
		}

		public CrmException(string message) : base(message)
		{
		}

		public CrmException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
