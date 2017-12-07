namespace CRM.EventSourcing
{
	public class DomainEventBus : IDomainEventBus, ISingleton
	{
		private readonly IDomainEventHandlerCatalog _domainEventHandlerCatalog;

		public DomainEventBus(IDomainEventHandlerCatalog domainEventHandlerCatalog)
		{
			_domainEventHandlerCatalog = domainEventHandlerCatalog;
		}

		public void Publish(IDomainEvent @event)
		{
			var eventType = @event.GetType();
			var eventHandlers = _domainEventHandlerCatalog.GetHandlers(eventType);

			foreach (var handler in eventHandlers)
			{
				InvokeHandler(handler, @event);
			}
		}


		private static void InvokeHandler(IDomainEventHandler eventHandler, IDomainEvent @event)
		{
			eventHandler.GetType()
									.GetMethod("Handle", new[] { @event.GetType() })
									.Invoke(eventHandler, new object[] { @event });
		}


	}
}