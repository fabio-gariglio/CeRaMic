namespace CRM.EventSourcing
{
	public interface IDomainCommandBus
	{
		void Send(IDomainCommand command);
	}
}
