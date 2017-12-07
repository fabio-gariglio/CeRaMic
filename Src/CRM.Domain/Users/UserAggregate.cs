using System;
using CRM.EventSourcing;
using CRM.Extensions;
using CRM.Users;
using CRM.Users.Events;

namespace CRM.Domain.Users
{
	public class UserAggregate : AggregateRootBase
	{
		public UserAggregate(){}
		public UserAggregate(string email, string password, string name, string role)
		{
			ApplyChange(new UserCreated(Guid.NewGuid(), email, password, name, role));
		}

		public void ChangePassword(string password)
		{
			ApplyChange(new UserPasswordChanged(Id, password));
		}

		public void RecoveryPassword()
		{
			var newPassword = StringExtension.Randomize(10);

			ApplyChange(new UserPasswordRecovered(Id, newPassword));
		}

		private void Apply(UserCreated @event)
		{
			Id = @event.AggregateId;
		}
	}
}
