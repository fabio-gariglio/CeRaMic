using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationPartnerSuggested : DomainEvent, IRelationSuggested, IRelationPartnerEvent
	{
		public Guid PartnerId { get; set; }

		public RelationPartnerSuggested(Guid aggregateId, Guid partnerId)
			: base(aggregateId)
		{
			PartnerId = partnerId;
		}
	}
}