using System;

namespace CRM.Exceptions
{
	public class RelationNotFoundException : CrmException
	{
		public RelationNotFoundException()
		{
		}

		public RelationNotFoundException(string message)
			: base(message)
		{
		}

		public RelationNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public string Name { get; set; }
	}
}