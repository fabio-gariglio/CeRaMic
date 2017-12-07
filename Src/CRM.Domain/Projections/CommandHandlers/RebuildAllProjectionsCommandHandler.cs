using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using CRM.Domain.Clients.Projections;
using CRM.Domain.Referents.Projections;
using CRM.Domain.Relations.Projections;
using CRM.Domain.Users.Projections;
using CRM.Domain.Utility;
using CRM.EventSourcing;
using CRM.Projections.Commands;

namespace CRM.Domain.Projections.CommandHandlers
{
	public class RebuildAllProjectionsCommandHandler : IDomainCommandHandler<RebuildAllProjectionsCommand>
	{
		private readonly IEventStore _eventStore;
		private readonly IProjectionCatalog _projectionCatalog;

		public RebuildAllProjectionsCommandHandler(IEventStore eventStore, IProjectionCatalog projectionCatalog)
		{
			_eventStore = eventStore;
			_projectionCatalog = projectionCatalog;
		}

		public void Handle(RebuildAllProjectionsCommand command)
		{
			RebuildProjections();
		}

		private void RebuildProjections()
		{
			var projections = new List<IProjection>
			                  {
				                  _projectionCatalog.GetProjection<NamesProjection>(),
													_projectionCatalog.GetProjection<UserProjection>(),
													_projectionCatalog.GetProjection<ClientProjection>(),
													_projectionCatalog.GetProjection<ReferentProjection>(),
													_projectionCatalog.GetProjection<RelationsProjection>()
			                  };

			projections.ForEach(p => p.Truncate());

			foreach (var @event in _eventStore.GetAll())
			{
				foreach (var projection in projections)
				{
					if(!HandleEvent(projection, @event)) continue;

					InvokeHandler(projection, @event);
				}

			}
		}

		private static bool HandleEvent(IProjection projection, IDomainEvent @event)
		{
			return projection.GetType()
			                 .GetInterfaces()
			                 .SelectMany(i => i.GetGenericArguments())
			                 .Any(t => t.IsInstanceOfType(@event));
		}

		private static void InvokeHandler(IDomainEventHandler eventHandler, IDomainEvent @event)
		{
			eventHandler.GetType()
									.GetMethod("Handle", new[] { @event.GetType() })
									.Invoke(eventHandler, new object[] { @event });
		}

	}
}
