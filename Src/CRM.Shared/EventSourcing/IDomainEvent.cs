using System;

namespace CRM.EventSourcing
{
	public interface IDomainEvent
	{
		Guid AggregateId { get; }
		Guid CommandId { get; set; }
		Guid UserId { get; set; }
	}
}