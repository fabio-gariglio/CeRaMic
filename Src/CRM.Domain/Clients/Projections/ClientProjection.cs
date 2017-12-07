using System;
using CRM.Clients;
using CRM.Clients.Events;
using CRM.Data;
using CRM.EventSourcing;

namespace CRM.Domain.Clients.Projections
{
	[HandlerPriority(100)]
	public class ClientProjection :
		IProjection,
		IDomainEventHandler<ClientCreated>,
		IDomainEventHandler<ClientNameChanged>
	{
		private readonly IClientRepository _repository;

		public ClientProjection(IClientRepository repository)
		{
			_repository = repository;
		}

		public void Handle(ClientCreated @event)
		{
			var contract = new ClientContract
			               {
				               Id = @event.AggregateId,
				               Name = @event.Name
			               };

			_repository.Insert(contract);
		}

		public void Handle(ClientNameChanged @event)
		{
			UpdateContract(@event, client => client.Name = @event.Name);
		}

		public void Truncate()
		{
			_repository.Clear();
		}

		private void UpdateContract(IDomainEvent @event, Action<ClientContract> updateAction)
		{
			var contract = _repository.GetById(@event.AggregateId);

			updateAction(contract);

			_repository.Update(contract);
		}
	}
}