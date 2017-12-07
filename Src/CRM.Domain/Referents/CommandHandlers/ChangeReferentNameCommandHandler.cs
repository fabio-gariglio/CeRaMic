using CRM.EventSourcing;
using CRM.Referents;
using CRM.Referents.Commands;

namespace CRM.Domain.Referents.CommandHandlers
{
	public class ChangeReferentNameCommandHandler : IDomainCommandHandler<ChangeReferentNameCommand>
	{
		private readonly IAggregateRepository<ReferentAggregate> _repository;
		private readonly IReferentAssertion _referentAssertion;

		public ChangeReferentNameCommandHandler(IAggregateRepository<ReferentAggregate> repository,
		                                     IReferentAssertion referentAssertion)
		{
			_repository = repository;
			_referentAssertion = referentAssertion;
		}

		public void Handle(ChangeReferentNameCommand command)
		{
			_referentAssertion.HasUniqueName(command.FirstName, command.LastName, command.Id);

			var aggregate = _repository.Find(command.Id);

			aggregate.ChangeName(command.FirstName, command.LastName);

			_repository.Save(aggregate, command);
		}
	}
}