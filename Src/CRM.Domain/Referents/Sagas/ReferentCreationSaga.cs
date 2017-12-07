using System;
using CRM.EventSourcing;
using CRM.Referents.Events;
using CRM.Relations.Commands;

namespace CRM.Domain.Referents.Sagas
{
	public class ReferentCreationSaga : ISaga, IDomainEventHandler<ReferentCreated>
	{
		private readonly IDomainCommandBus _commandBus;

		public ReferentCreationSaga(IDomainCommandBus commandBus)
		{
			_commandBus = commandBus;
		}

		public void Handle(ReferentCreated @event)
		{
			var createRelationCommand = new CreateRelationCommand(@event.AggregateId)
			                               {
				                               CommandId = Guid.NewGuid(),
				                               UserId = @event.UserId
			                               };

			_commandBus.Send(createRelationCommand);
		}
	}
}
