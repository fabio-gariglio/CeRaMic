using CRM.EventSourcing;
using CRM.Users.Commands;

namespace CRM.Domain.Users.CommandHandlers
{
	public class RecoveryUserPasswordCommandHandler : IDomainCommandHandler<RecoveryUserPasswordCommand>
	{
		private readonly IAggregateRepository<UserAggregate> _repository;

		public RecoveryUserPasswordCommandHandler(IAggregateRepository<UserAggregate> repository)
		{
			_repository = repository;
		}

		public void Handle(RecoveryUserPasswordCommand command)
		{
			var aggregate = _repository.Find(command.UserId);

			aggregate.RecoveryPassword();

			_repository.Save(aggregate, command);
		}
	}
}
