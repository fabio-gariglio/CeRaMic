using System;
using System.Collections.Generic;
using CRM.Reflection;

namespace CRM.EventSourcing
{
	public abstract class AggregateRootBase : IAggregateRoot
	{
		public const StringComparison DefaultComparison = StringComparison.InvariantCultureIgnoreCase ;
		public Guid Id { get; protected set; }
		public IList<IDomainEvent> UncommittedChanges { get; private set; }

		protected AggregateRootBase()
		{
			UncommittedChanges = new List<IDomainEvent>();
		}

		public void LoadHistory(IEnumerable<IDomainEvent> events)
		{
			foreach (var @event in events)
			{
				ApplyChange(@event, true);
			}
		}

		protected void ApplyChange(IDomainEvent @event)
		{
			ApplyChange(@event, false);
		}

		private void ApplyChange(IDomainEvent @event, bool committed)
		{
			this.Invoke("Apply", @event);

			if (committed) return;

			UncommittedChanges.Add(@event);
		}
	}
}
