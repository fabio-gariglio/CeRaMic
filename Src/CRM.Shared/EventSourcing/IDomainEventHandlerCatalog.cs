using System;
using System.Collections.Generic;

namespace CRM.EventSourcing
{
	public interface IDomainEventHandlerCatalog
	{
		IEnumerable<IDomainEventHandler> GetHandlers(Type eventType);
	}
}