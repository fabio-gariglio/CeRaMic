using System;
using CRM.Data;
using CRM.EventSourcing;
using CRM.Extensions;
using CRM.Referents;
using CRM.Referents.Events;

namespace CRM.Domain.Referents.Projections
{
	[HandlerPriority(100)]
	public class ReferentProjection :
		IProjection,
		IDomainEventHandler<ReferentCreated>,
		IDomainEventHandler<ReferentNameChanged>,
		IDomainEventHandler<ReferentClientSet>,
		IDomainEventHandler<ReferentAreaSet>,
		IDomainEventHandler<ReferentEmailContactSet>,
		IDomainEventHandler<ReferentLandlineContactSet>,
		IDomainEventHandler<ReferentMobileContactSet>,
		IDomainEventHandler<ReferentSecretarySet>

	{
		private readonly IReferentRepository _repository;

		public ReferentProjection(IReferentRepository repository)
		{
			_repository = repository;
		}

		public void Handle(ReferentCreated @event)
		{
			var contract = new ReferentContract
			{
				Id = @event.AggregateId,
				FirstName = StringExtension.ToCamelCase(@event.FirstName),
				LastName = StringExtension.ToCamelCase(@event.LastName),
				Name = StringExtension.BuildFullName(@event.FirstName, @event.LastName)
			};

			_repository.Insert(contract);
		}

		public void Handle(ReferentNameChanged @event)
		{
			UpdateContract(@event,
										 client =>
										 {
											 client.FirstName = StringExtension.ToCamelCase(@event.FirstName);
											 client.LastName = StringExtension.ToCamelCase(@event.LastName);
											 client.Name = StringExtension.BuildFullName(@event.FirstName, @event.LastName);
										 });
		}

		public void Handle(ReferentClientSet @event)
		{
			UpdateContract(@event, client => client.ClientId = @event.ClientId);
		}

		public void Handle(ReferentAreaSet @event)
		{
			UpdateContract(@event, client => client.Area = @event.Area);
		}

		public void Handle(ReferentEmailContactSet @event)
		{
			UpdateContract(@event, client => client.EmailAddress = @event.Address);
		}

		public void Handle(ReferentLandlineContactSet @event)
		{
			UpdateContract(@event, client => client.LandlineNumber = @event.Number);
		}

		public void Handle(ReferentMobileContactSet @event)
		{
			UpdateContract(@event, client => client.MobilePhone = @event.Number);
		}

		public void Handle(ReferentSecretarySet @event)
		{
			UpdateContract(@event, client => client.Secretary = @event.Secretary);
		}

		public void Truncate()
		{
			_repository.Clear();
		}

		private void UpdateContract(IDomainEvent @event, Action<ReferentContract> updateAction)
		{
			var contract = _repository.GetById(@event.AggregateId);

			updateAction(contract);

			_repository.Update(contract);
		}
	}
}