using System;
using CRM.Common;
using CRM.Data;
using CRM.EventSourcing;
using CRM.Users;
using CRM.Users.Events;

namespace CRM.Domain.Users.Projections
{
	[HandlerPriority(100)]
	public class UserProjection : 
		IProjection,
		IDomainEventHandler<UserCreated>,
		IDomainEventHandler<UserPasswordChanged>,
		IDomainEventHandler<UserPasswordRecovered>
	{
		private readonly IUserRepository _repository;
		private readonly IEncriptionService _encriptionService;

		public UserProjection(IUserRepository repository, IEncriptionService encriptionService)
		{
			_repository = repository;
			_encriptionService = encriptionService;
		}

		public void Handle(UserCreated @event)
		{
			var passwordHash = _encriptionService.CalculateHash(@event.Password);

			var contract = new UserContract
			               {
				               Id = @event.AggregateId,
											 Email = @event.Email,
											 PasswordHash = passwordHash,
											 Name = @event.Name,
											 Role = @event.Role
			               };

			_repository.Insert(contract);
		}

		public void Handle(UserPasswordChanged @event)
		{
			var passwordHash = _encriptionService.CalculateHash(@event.Password);

			UpdateContract(@event, user => user.PasswordHash = passwordHash);
		}

		public void Handle(UserPasswordRecovered @event)
		{
			var passwordHash = _encriptionService.CalculateHash(@event.Password);

			UpdateContract(@event, user => user.PasswordHash = passwordHash);
		}

		public void Truncate()
		{
			_repository.Clear();
		}

		protected void UpdateContract(IDomainEvent @event, Action<UserContract> updateAction)
		{
			var contract = _repository.GetById(@event.AggregateId);

			updateAction(contract);

			_repository.Update(contract);
		}
	}
}
