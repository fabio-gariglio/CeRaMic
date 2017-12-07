using CRM.EventSourcing;
using CRM.Relations.Commands;
using CRM.Users;

namespace CRM.Domain.Relations.CommandHandlers
{
	public class CreateRelationCommandHandler : IDomainCommandHandler<CreateRelationCommand>
	{
		private readonly IAggregateRepository<RelationAggregate> _repository;
		private readonly IUserCatalog _userCatalog;

		public CreateRelationCommandHandler(IAggregateRepository<RelationAggregate> repository, IUserCatalog userCatalog)
		{
			_repository = repository;
			_userCatalog = userCatalog;
		}

		public void Handle(CreateRelationCommand command)
		{
			var aggregate = new RelationAggregate(command.ReferentId);

			if (IsSellerCommand(command))
			{
				aggregate.SetOwner(command.UserId);
			}

			_repository.Save(aggregate, command);
		}

		private bool IsSellerCommand(IDomainCommand command)
		{
			var user = _userCatalog[command.UserId];

			return null != user && user.Role == UserRoles.Seller;
		}
	}
}
