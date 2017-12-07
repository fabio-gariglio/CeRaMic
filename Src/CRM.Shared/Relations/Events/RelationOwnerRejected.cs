using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationOwnerRejected : DomainEvent, IRelationRejected, IRelationOwnerEvent
	{
		public Guid OwnerId { get; set; }

		public RelationOwnerRejected(Guid aggregateId, Guid ownerId)
			: base(aggregateId)
		{
			OwnerId = ownerId;
		}
	}
}