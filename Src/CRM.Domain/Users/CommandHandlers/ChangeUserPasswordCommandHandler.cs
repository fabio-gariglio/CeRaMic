using CRM.EventSourcing;
using CRM.Users.Commands;

namespace CRM.Domain.Users.CommandHandlers
{
	public class ChangeUserPasswordCommandHandler : IDomainCommandHandler<ChangeUserPasswordCommand>
	{
		private readonly IAggregateRepository<UserAggregate> _repository;

		public ChangeUserPasswordCommandHandler(IAggregateRepository<UserAggregate> repository)
		{
			_repository = repository;
		}

		public void Handle(ChangeUserPasswordCommand command)
		{
			var aggregate = _repository.Find(command.UserId);

			aggregate.ChangePassword(command.Password);

			_repository.Save(aggregate, command);
		}
	}
}
