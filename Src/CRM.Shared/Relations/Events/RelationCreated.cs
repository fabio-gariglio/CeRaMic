using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationCreated : DomainEvent, IRelationEvent
	{
		public Guid ReferentId { get; set; }

		public RelationCreated(Guid aggregateId, Guid referentId) 
			: base(aggregateId)
		{
			ReferentId = referentId;
		}
	}
}
