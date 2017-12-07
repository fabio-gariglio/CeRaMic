namespace CRM.EventSourcing
{
	public interface IDomainEventHandler
	{
	}

	public interface IDomainEventHandler<in TEvent> : IDomainEventHandler
		where TEvent : IDomainEvent
	{
		void Handle(TEvent @event);
	}
}
