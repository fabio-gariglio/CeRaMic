using System;
using CRM.Data;
using CRM.Domain.Clients;
using CRM.EventSourcing;
using CRM.Referents;
using CRM.Referents.Commands;

namespace CRM.Domain.Referents.CommandHandlers
{
	public class ReferentCommandHandler : 
		IDomainCommandHandler<CreateReferentCommand>,
		IDomainCommandHandler<UpdateReferentCommand>
	{
		private readonly IAggregateRepository<ReferentAggregate> _referentAggregateRepository;
		private readonly IAggregateRepository<ClientAggregate> _clientAggregateRepository;
		private readonly IClientRepository _clientRepository;
		private readonly IReferentAssertion _referentAssertion;

		public ReferentCommandHandler(
			IAggregateRepository<ReferentAggregate> referentAggregateRepository, 
			IAggregateRepository<ClientAggregate> clientAggregateRepository,
			IClientRepository clientRepository,
			IReferentAssertion referentAssertion)
		{
			_referentAggregateRepository = referentAggregateRepository;
			_clientAggregateRepository = clientAggregateRepository;
			_clientRepository = clientRepository;
			_referentAssertion = referentAssertion;
		}

		public void Handle(CreateReferentCommand command)
		{
			_referentAssertion.HasUniqueName(command.FirstName, command.LastName, Guid.Empty);

			var aggregate = new ReferentAggregate(command.FirstName, command.LastName);

			SetupReferentAggreate(aggregate, command);
		}

		public void Handle(UpdateReferentCommand command)
		{
			_referentAssertion.HasUniqueName(command.FirstName, command.LastName, command.Id);

			var aggregate = _referentAggregateRepository.Find(command.Id);

			aggregate.ChangeName(command.FirstName, command.LastName);

			SetupReferentAggreate(aggregate, command);
		}

		private void SetupReferentAggreate(ReferentAggregate aggregate, CreateReferentCommand command)
		{
			var clientId = GetOrAddClientByName(command.ClientName, command);

			aggregate.SetClient(clientId);
			aggregate.SetArea(command.Area);
			aggregate.SetEmailContact(command.EmailAddress);
			aggregate.SetMobileContact(command.MobilePhone);
			aggregate.SetLandlineContact(command.LandlineNumber);

			_referentAggregateRepository.Save(aggregate, command);
		}

		private Guid GetOrAddClientByName(string name, IDomainCommand command)
		{
			var client = _clientRepository.GetByName(name);

			if (null != client) return client.Id;

			var aggregate = new ClientAggregate(name);

			_clientAggregateRepository.Save(aggregate, command);

			return aggregate.Id;
		}
	}
}
