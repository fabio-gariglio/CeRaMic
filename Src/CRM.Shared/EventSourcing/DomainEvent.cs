using System;
using System.Security.Cryptography.X509Certificates;

namespace CRM.EventSourcing
{
	public abstract class DomainEvent : IDomainEvent
	{
		public Guid AggregateId { get; private set; }
		public Guid CommandId { get; set; }
		public Guid UserId { get; set; }
		public DateTime UtcDateTime { get; set; }

		protected DomainEvent(Guid aggregateId)
		{
			AggregateId = aggregateId;
			UtcDateTime = DateTime.UtcNow;
		}
	}
}