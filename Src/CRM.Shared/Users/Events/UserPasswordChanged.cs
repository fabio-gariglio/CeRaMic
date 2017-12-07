using System;
using CRM.EventSourcing;

namespace CRM.Users.Events
{
	public class UserPasswordChanged : DomainEvent
	{
		public string Password { get; private set; }

		public UserPasswordChanged(Guid aggregateId, string password)
			: base(aggregateId)
		{
			Password = password;
		}
	}
}
