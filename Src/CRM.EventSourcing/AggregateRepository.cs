using System;
using System.Linq;

namespace CRM.EventSourcing
{
	public class AggregateRepository<TAggregate> : IAggregateRepository<TAggregate>, ISingleton
		where TAggregate : class, IAggregateRoot, new()
	{
		private readonly IEventStore _eventStore;

		public AggregateRepository(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public TAggregate Find(Guid id)
		{
			var events = _eventStore.Read(id).ToList();

			if (!events.Any()) return null;

			var aggregate = new TAggregate();

			aggregate.LoadHistory(events);

			return aggregate;
		}

		public void Save(TAggregate aggregate, IDomainCommand sourceCommand)
		{
			if (!aggregate.UncommittedChanges.Any()) return;

			_eventStore.Save(aggregate.Id, aggregate.UncommittedChanges, sourceCommand);
		}
	}
}
