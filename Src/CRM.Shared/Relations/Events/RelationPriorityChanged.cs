using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationPriorityChanged : DomainEvent
	{
		public int Priority { get; set; }

		public RelationPriorityChanged(Guid aggregateId, int priority)
			: base(aggregateId)
		{
			Priority = priority;
		}
	}
}
