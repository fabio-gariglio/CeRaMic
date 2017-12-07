using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Castle.Core;
using NEventStore;
using NEventStore.Dispatcher;
using NEventStore.Persistence.Sql;
using NEventStore.Persistence.Sql.SqlDialects;

namespace CRM.EventSourcing
{
	public class EventStore : IEventStore, ISingleton
	{
		private readonly IConnectionFactory _connectionFactory;
		private readonly IDispatchCommits _dispatchCommits;
		private readonly Lazy<IStoreEvents> _store;

		public EventStore(IConnectionFactory connectionFactory, IDispatchCommits dispatchCommits)
		{
			_connectionFactory = connectionFactory;
			_dispatchCommits = dispatchCommits;
			_store = new Lazy<IStoreEvents>(StoreEventsFactory);
		}

		public void Save(Guid aggregateId, IEnumerable<IDomainEvent> domainEvents, IDomainCommand sourceCommand)
		{
			foreach (var @event in domainEvents)
			{
				using (var stream = _store.Value.OpenStream(aggregateId))
				{
					@event.CommandId = sourceCommand.CommandId;
					@event.UserId = sourceCommand.UserId;

					stream.Add(new EventMessage {Body = @event});
					stream.CommitChanges(Guid.NewGuid());
				}
			}
		}

		public IEnumerable<IDomainEvent> Read(Guid aggregateId)
		{
			using (var stream = _store.Value.OpenStream(aggregateId, 0))
			{
				return stream.CommittedEvents.Select(AsDomainEvent);
			}
		}

		public IEnumerable<IDomainEvent> GetAll()
		{
			return _store.Value
			             .Advanced
			             .GetFrom()
			             .SelectMany(c => c.Events)
			             .Select(AsDomainEvent);
		}

		public IStoreEvents StoreEventsFactory()
		{
			return Wireup.Init()
									 .UsingSqlPersistence(_connectionFactory)
									 .WithDialect(new MsSqlDialect())
									 .UsingJsonSerialization()
			             .UsingSynchronousDispatchScheduler()
			             .DispatchTo(_dispatchCommits)
			             .Build();
		}

		private static IDomainEvent AsDomainEvent(EventMessage eventMessage)
		{
			return (IDomainEvent) eventMessage.Body;
		}
	}
}