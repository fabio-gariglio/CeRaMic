using System;

namespace CRM.Exceptions
{
	public class DuplicatedClientException : CrmException
	{
		public DuplicatedClientException()
		{
		}

		public DuplicatedClientException(string message)
			: base(message)
		{
		}

		public DuplicatedClientException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public string Name { get; set; }
	}
}