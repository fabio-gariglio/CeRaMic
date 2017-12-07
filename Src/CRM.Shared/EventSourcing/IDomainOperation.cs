using System.Collections.Generic;

namespace CRM.EventSourcing
{
	public interface IDomainOperation<out TCommand>
		where TCommand : IDomainCommand
	{
		TCommand Command { get; }

		IEnumerable<IDomainEvent> Events { get; }
	}
}
