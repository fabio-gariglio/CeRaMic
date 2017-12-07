using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Referents.Events
{
	public class ReferentMobileContactSet : DomainEvent
	{
		public string Number { get; set; }

		public ReferentMobileContactSet(Guid aggregateId, string number)
			: base(aggregateId)
		{
			Number = number;
		}
	}
}
