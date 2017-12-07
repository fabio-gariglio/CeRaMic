using System;
using ServiceStack.DataAnnotations;

namespace CRM.Data.Entities
{
	[Alias("Names")]
	public class NameEntity
	{
		[PrimaryKey]
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
