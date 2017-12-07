using System;
using System.Collections.Generic;

namespace CRM.EventSourcing
{
	public interface IAggregateRoot
	{
		Guid Id { get; }

		IList<IDomainEvent> UncommittedChanges { get; }

		void LoadHistory(IEnumerable<IDomainEvent> events);
	}
}