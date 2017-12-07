using System;
using System.ComponentModel;

namespace CRM.Users
{
	[DisplayName("Users")]
	public class UserContract : IContract
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
	}
}
