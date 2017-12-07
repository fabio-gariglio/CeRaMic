using CRM.Clients;
using CRM.Clients.Commands;
using CRM.EventSourcing;

namespace CRM.Domain.Clients.CommandHandlers
{
	public class ChangeClientNameCommandHandler : IDomainCommandHandler<ChangeClientNameCommand>
	{
		private readonly IAggregateRepository<ClientAggregate> _repository;
		private readonly IClientAssertion _clientAssertion;

		public ChangeClientNameCommandHandler(IAggregateRepository<ClientAggregate> repository,
		                                      IClientAssertion clientAssertion)
		{
			_repository = repository;
			_clientAssertion = clientAssertion;
		}

		public void Handle(ChangeClientNameCommand command)
		{
			_clientAssertion.HasUniqueName(command.Name);

			var aggregate = _repository.Find(command.Id);

			aggregate.ChangeName(command.Name);

			_repository.Save(aggregate, command);
		}
	}
}