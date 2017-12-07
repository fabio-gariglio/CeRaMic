using System;
using System.ComponentModel;

namespace CRM.Referents
{
	[DisplayName("Referents")]
	public class ReferentContract : IContract
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Guid ClientId { get; set; }
		public string Area { get; set; }
		public string EmailAddress { get; set; }
		public string LandlineNumber { get; set; }
		public string MobilePhone { get; set; }
		public string Secretary { get; set; }
	}
}
