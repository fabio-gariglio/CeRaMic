using System;
using CRM.EventSourcing;

namespace CRM.Referents.Events
{
	public class ReferentAreaSet : DomainEvent
	{
		public string Area { get; set; }
		public ReferentAreaSet(Guid aggregateId, string area)
			: base(aggregateId)
		{
			Area = area;
		}
	}
}