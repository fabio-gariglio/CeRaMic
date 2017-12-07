using System;
using CRM.EventSourcing;

namespace CRM.Clients.Events
{
	public class ClientCreated : DomainEvent
	{
		public string Name { get; set; }

		public ClientCreated(Guid aggregateId, string name) 
			: base(aggregateId)
		{
			Name = name;
		}
	}
}