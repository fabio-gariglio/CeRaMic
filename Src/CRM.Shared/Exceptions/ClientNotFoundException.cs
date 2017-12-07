using System;

namespace CRM.Exceptions
{
	public class ClientNotFoundException : CrmException
	{
		public ClientNotFoundException()
		{
		}

		public ClientNotFoundException(string message)
			: base(message)
		{
		}

		public ClientNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public string Name { get; set; }
	}
}