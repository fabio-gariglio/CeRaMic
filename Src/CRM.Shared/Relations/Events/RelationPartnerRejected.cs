using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationPartnerRejected : DomainEvent, IRelationRejected, IRelationPartnerEvent
	{
		public Guid PartnerId { get; set; }

		public RelationPartnerRejected(Guid aggregateId, Guid partnerId)
			: base (aggregateId)
		{
			PartnerId = partnerId;
		}
	}
}