using System;

namespace CRM.Utility
{
	public class NameContract : IContract
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
