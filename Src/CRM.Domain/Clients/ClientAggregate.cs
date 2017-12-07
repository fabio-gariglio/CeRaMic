using System;
using CRM.Clients.Events;
using CRM.EventSourcing;

namespace CRM.Domain.Clients
{
	public class ClientAggregate : AggregateRootBase
	{
		private string Name { get; set; }

		public ClientAggregate() { }

		public ClientAggregate(string name)
		{
			ApplyChange(new ClientCreated(Guid.NewGuid(), name));
		}

		public void ChangeName(string name)
		{
			if (string.Equals(name, Name, DefaultComparison)) return;

			ApplyChange(new ClientNameChanged(Id, name));
		}

		#region Events Handling

		private void Apply(ClientCreated @event)
		{
			Id = @event.AggregateId;
		}

		private void Apply(ClientNameChanged @event)
		{
			Name = @event.Name;
		}

		#endregion
	}
}
