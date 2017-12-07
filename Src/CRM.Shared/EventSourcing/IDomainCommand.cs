using System;

namespace CRM.EventSourcing
{
	public interface IDomainCommand
	{
		Guid CommandId { get; set; }

		Guid UserId { get; set; }
	}
}
