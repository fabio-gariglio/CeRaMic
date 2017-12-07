using System;
using System.Collections.Generic;

namespace CRM.EventSourcing
{
	public interface IEventStore
	{
		void Save(Guid aggregateId, IEnumerable<IDomainEvent> domainEvents, IDomainCommand sourceCommand);
		IEnumerable<IDomainEvent> Read(Guid aggregateId);
		IEnumerable<IDomainEvent> GetAll();
	}
}