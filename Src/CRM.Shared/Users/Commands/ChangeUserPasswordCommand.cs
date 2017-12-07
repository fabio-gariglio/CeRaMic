using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Users.Commands
{
	public class ChangeUserPasswordCommand : DomainCommand
	{
		public string Password { get; private set; }

		public ChangeUserPasswordCommand(Guid userId, string password)
		{
			Condition.Requires(userId, "userId").IsNotEqualTo(Guid.Empty);
			Condition.Requires(password, "password").IsNotNullOrEmpty();

			UserId = userId;
			Password = password;
		}
	}
}
