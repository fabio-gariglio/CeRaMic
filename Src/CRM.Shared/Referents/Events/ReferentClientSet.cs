using System;
using CRM.EventSourcing;

namespace CRM.Referents.Events
{
	public class ReferentClientSet : DomainEvent
	{
		public Guid ClientId { get; set; }

		public ReferentClientSet(Guid aggregateId, Guid clientId)
			: base(aggregateId)
		{
			ClientId = clientId;
		}
	}
}