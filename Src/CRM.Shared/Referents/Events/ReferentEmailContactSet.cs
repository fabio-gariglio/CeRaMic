using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Referents.Events
{
	public class ReferentEmailContactSet : DomainEvent
	{
		public string Address { get; set; }

		public ReferentEmailContactSet(Guid aggregateId, string address)
			: base(aggregateId)
		{
			Address = address;
		}
	}
}
