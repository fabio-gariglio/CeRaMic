using System;
using System.ComponentModel;

namespace CRM.Clients
{
	[DisplayName("Clients")]
	public class ClientContract : IContract
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
