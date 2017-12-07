namespace CRM.EventSourcing
{
	public interface IDomainOperationHandler
	{
	}

	public interface IDomainOperationHandler<in TOperation, TCommand> : IDomainOperationHandler
		where TOperation : IDomainOperation<TCommand>
		where TCommand : IDomainCommand
	{
		void Handle<TOperation>(TOperation operation);
	}
}