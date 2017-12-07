using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Referents.Events
{
	public class ReferentLandlineContactSet : DomainEvent
	{
		public string Number { get; set; }

		public ReferentLandlineContactSet(Guid aggregateId, string number) : base(aggregateId)
		{
			Number = number;
		}
	}
}
