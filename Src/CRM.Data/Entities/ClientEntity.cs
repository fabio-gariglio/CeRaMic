using System;
using CRM.Data.Common;
using ServiceStack.DataAnnotations;

namespace CRM.Data.Entities
{
	[Alias("Clients")]
	public class ClientEntity
	{
		[PrimaryKey]
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
