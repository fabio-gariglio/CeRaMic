using CRM.EventSourcing;
using CRM.Users.Commands;

namespace CRM.Domain.Users.CommandHandlers
{
	public class CreateUserCommandHandler : IDomainCommandHandler<CreateUserCommand>
	{
		private readonly IAggregateRepository<UserAggregate> _repository;

		public CreateUserCommandHandler(IAggregateRepository<UserAggregate> repository)
		{
			_repository = repository;
		}

		public void Handle(CreateUserCommand command)
		{
			var aggregate = new UserAggregate(command.Email, command.Password, command.Name, command.Role);

			aggregate.ChangePassword(command.Password);

			_repository.Save(aggregate, command);
		}
	}
}
