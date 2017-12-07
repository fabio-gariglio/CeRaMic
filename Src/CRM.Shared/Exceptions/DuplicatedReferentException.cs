using System;

namespace CRM.Exceptions
{
	public class DuplicatedReferentException : CrmException
	{
		public DuplicatedReferentException()
		{
		}

		public DuplicatedReferentException(string name)
			: base(string.Format("A referent named '{0}' already exists.", name))
		{
			Name = name;
		}

		public DuplicatedReferentException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public string Name { get; set; }
	}
}