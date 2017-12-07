using CRM.Clients;
using CRM.Clients.Commands;
using CRM.EventSourcing;

namespace CRM.Domain.Clients.CommandHandlers
{
	public class CreateClientCommandHandler : IDomainCommandHandler<CreateClientCommand>
	{
		private readonly IAggregateRepository<ClientAggregate> _aggregateRepository;
		private readonly IClientAssertion _clientAssertion;

		public CreateClientCommandHandler(IAggregateRepository<ClientAggregate> aggregateRepository,
		                                  IClientAssertion clientAssertion)
		{
			_aggregateRepository = aggregateRepository;
			_clientAssertion = clientAssertion;
		}

		public void Handle(CreateClientCommand command)
		{
			_clientAssertion.HasUniqueName(command.Name);

			var aggregate = new ClientAggregate(command.Name);

			_aggregateRepository.Save(aggregate, command);
		}
	}
}