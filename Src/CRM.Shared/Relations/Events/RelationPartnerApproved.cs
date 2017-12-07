using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationPartnerApproved : DomainEvent, IRelationApproved, IRelationPartnerEvent
	{
		public Guid PartnerId { get; set; }

		public RelationPartnerApproved(Guid aggregateId, Guid partnerId)
			: base(aggregateId)
		{
			PartnerId = partnerId;
		}
	}
}