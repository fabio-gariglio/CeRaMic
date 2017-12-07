using System;
using CRM.Data.Common;
using ServiceStack.DataAnnotations;

namespace CRM.Data.Entities
{
	[Alias("Users")]
	public class UserEntity
	{
		[PrimaryKey]
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
	}
}
