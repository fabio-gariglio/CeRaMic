namespace CRM.EventSourcing
{
	public interface IDomainCommandHandler
	{
	}

	public interface IDomainCommandHandler<in TCommand> : IDomainCommandHandler
		where TCommand : IDomainCommand
	{
		void Handle(TCommand command);
	}
}
