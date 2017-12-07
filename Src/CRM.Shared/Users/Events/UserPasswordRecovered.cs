using System;
using CRM.EventSourcing;

namespace CRM.Users.Events
{
	public class UserPasswordRecovered : DomainEvent
	{
		public string Password { get; private set; }

		public UserPasswordRecovered(Guid aggregateId, string password)
			: base(aggregateId)
		{
			Password = password;
		}
	}
}
