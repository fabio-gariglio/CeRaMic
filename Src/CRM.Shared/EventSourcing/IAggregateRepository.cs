using System;

namespace CRM.EventSourcing
{
	public interface IAggregateRepository<TAggregate>
		where TAggregate : IAggregateRoot, new()
	{
		TAggregate Find(Guid id);

		void Save(TAggregate aggregate, IDomainCommand sourceCommand);
	}
}