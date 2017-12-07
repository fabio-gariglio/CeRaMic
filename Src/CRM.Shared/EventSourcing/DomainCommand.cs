using System;

namespace CRM.EventSourcing
{
	public abstract class DomainCommand : IDomainCommand
	{
		public Guid CommandId { get; set; }

		public Guid UserId { get; set; }
	}
}
