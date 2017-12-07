using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Users.Commands
{
	public class RecoveryUserPasswordCommand : DomainCommand
	{
		public RecoveryUserPasswordCommand(Guid userId)
		{
			Condition.Requires(userId, "userId").IsNotEqualTo(Guid.Empty);

			UserId = userId;
		}
	}
}
