using System;
using CRM.EventSourcing;

namespace CRM.Clients.Events
{
	public class ClientNameChanged : DomainEvent
	{
		public string Name { get; private set; }

		public ClientNameChanged(Guid aggregateId, string name) 
			: base(aggregateId)
		{
			Name = name;
		}
	}
}