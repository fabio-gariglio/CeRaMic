namespace CRM.EventSourcing
{
	public interface IProjection : IDomainEventHandler
	{
		void Truncate();
	}
}