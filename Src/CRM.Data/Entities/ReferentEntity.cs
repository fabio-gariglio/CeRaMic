using System;
using ServiceStack.DataAnnotations;

namespace CRM.Data.Entities
{
	[Alias("Referents")]
	public class ReferentEntity
	{
		[PrimaryKey]
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
