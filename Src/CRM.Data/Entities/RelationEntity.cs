using System;
using ServiceStack.DataAnnotations;

namespace CRM.Data.Entities
{
	[Alias("Relations")]
	public class RelationEntity
	{
		[PrimaryKey]
		public Guid Id { get; set; }
		public Guid ReferentId { get; set; }
		public Guid ClientId { get; set; }
		public Guid OwnerId { get; set; }
		public Guid PartnerId { get; set; }
		public string ReferentName { get; set; }
		public string ClientName { get; set; }
		public string OwnerName { get; set; }
		public string PartnerName { get; set; }
		public string NotesJson { get; set; }
		public string EmailAddress { get; set; }
		public string LandlineNumber { get; set; }
		public string MobilePhone { get; set; }
		public int Priority { get; set; }
	}
}
