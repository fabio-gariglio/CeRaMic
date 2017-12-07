namespace CRM.EventSourcing
{
	public interface IDomainOperationBus
	{
		void Publish<TCommand>(IDomainOperation<TCommand> operation) 
			where TCommand : IDomainCommand;
	}
}
