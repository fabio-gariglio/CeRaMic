using System;
using CRM.EventSourcing;

namespace CRM.Users.Events
{
	public class UserCreated : DomainEvent
	{
		public string Email { get; private set; }
		public string Password { get; private set; }
		public string Name { get; private set; }
		public string Role { get; private set; }

		public UserCreated(Guid aggregateId, string email, string password, string name, string role) : base(aggregateId)
		{
			Email = email;
			Password = password;
			Name = name;
			Role = role;
		}
	}
}
