using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationOwnerSuggested : DomainEvent, IRelationSuggested, IRelationOwnerEvent
	{
		public Guid OwnerId { get; set; }

		public RelationOwnerSuggested(Guid aggregateId, Guid ownerId) 
			: base(aggregateId)
		{
			OwnerId = ownerId;
		}
	}
}
