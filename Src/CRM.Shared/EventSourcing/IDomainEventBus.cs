namespace CRM.EventSourcing
{
	public interface IDomainEventBus
	{
		void Publish(IDomainEvent @event);
	}
}
