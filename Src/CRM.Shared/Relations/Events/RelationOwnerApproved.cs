using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationOwnerApproved : DomainEvent, IRelationApproved, IRelationOwnerEvent
	{
		public Guid OwnerId { get; set; }

		public RelationOwnerApproved(Guid aggregateId, Guid ownerId)
			: base(aggregateId)
		{
			OwnerId = ownerId;
		}
	}
}