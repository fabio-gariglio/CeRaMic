using System;
using System.Linq;
using Castle.Core;
using CRM.Diagnostic;
using CRM.Exceptions;
using NEventStore;
using NEventStore.Dispatcher;

namespace CRM.EventSourcing
{
	public class Dispatcher : IDispatchCommits, INeedLogger ,ISingleton
	{
		private readonly IDomainEventBus _domainEventBus;
		public ILogger Logger { get; set; }

		public Dispatcher(IDomainEventBus domainEventBus)
		{
			_domainEventBus = domainEventBus;
		}

		public void Dispose()
		{
		}

		public void Dispatch(ICommit commit)
		{
			try
			{
				foreach (var @event in commit.Events.Select(e => e.Body as IDomainEvent))
				{
					_domainEventBus.Publish(@event);
				}
			}
			catch (CrmException crmException)
			{
				Logger.Error(new LogMessage("An error has occurred during the events dispatching.", crmException.ToString()));
			}
			catch (Exception exception)
			{
				Logger.Critical(new LogMessage("An error has occurred during the events dispatching.", exception.ToString()));
			}
		}

	}
}