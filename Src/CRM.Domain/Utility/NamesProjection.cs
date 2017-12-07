using CRM.Clients.Events;
using CRM.Data;
using CRM.EventSourcing;
using CRM.Extensions;
using CRM.Referents.Events;
using CRM.Users.Events;
using CRM.Utility;

namespace CRM.Domain.Utility
{
	[HandlerPriority(100)]
	public class NamesProjection : 
		IProjection,
		IDomainEventHandler<ClientCreated>,
		IDomainEventHandler<ClientNameChanged>,
		IDomainEventHandler<ReferentCreated>,
		IDomainEventHandler<ReferentNameChanged>,
		IDomainEventHandler<UserCreated>
	{
		private readonly INameRepository _repository;

		public NamesProjection(INameRepository repository)
		{
			_repository = repository;
		}

		public void Handle(ClientCreated @event)
		{
			_repository.Insert(new NameContract()
			                       {
				                       Id = @event.AggregateId,
				                       Name = @event.Name
			                       });
		}

		public void Handle(ClientNameChanged @event)
		{
			_repository.Update(new NameContract()
			{
				Id = @event.AggregateId,
				Name = @event.Name
			});
		}

		public void Handle(ReferentCreated @event)
		{
			_repository.Insert(new NameContract()
			                       {
				                       Id = @event.AggregateId,
				                       Name = StringExtension.BuildFullName(@event.FirstName, @event.LastName)
			                       });
		}

		public void Handle(ReferentNameChanged @event)
		{
			_repository.Update(new NameContract()
			{
				Id = @event.AggregateId,
				Name = StringExtension.BuildFullName(@event.FirstName, @event.LastName)
			});
		}

		public void Handle(UserCreated @event)
		{
			_repository.Insert(new NameContract()
			{
				Id = @event.AggregateId,
				Name = @event.Name
			});
		}
		
		public void Truncate()
		{
			_repository.Clear();
		}
	}
}
