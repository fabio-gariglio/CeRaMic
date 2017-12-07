using CRM.EventSourcing;
using Microsoft.AspNet.SignalR;

namespace CRM.WebSite.SignalR
{
	public class DomainEventHandler : IDomainEventHandler<IDomainEvent>
	{
		private readonly IHubContext _context;

		public DomainEventHandler(IHubContext context)
		{
			 _context = context;
		}

		public void Handle(IDomainEvent @event)
		{
			OnEventRaised(@event);
		}

		private void OnEventRaised(IDomainEvent @event)
		{
			_context.Clients.All.onEventRaised(new {@event.GetType().Name, Data = @event});
		}
	}
}