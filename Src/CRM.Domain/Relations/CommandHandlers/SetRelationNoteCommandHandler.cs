using CRM.EventSourcing;
using CRM.Exceptions;
using CRM.Relations.Commands;

namespace CRM.Domain.Relations.CommandHandlers
{
	public class SetRelationNoteCommandHandler : IDomainCommandHandler<SetRelationNoteCommand>
	{
		private readonly IAggregateRepository<RelationAggregate> _repository;

		public SetRelationNoteCommandHandler(IAggregateRepository<RelationAggregate> repository)
		{
			_repository = repository;
		}

		public void Handle(SetRelationNoteCommand command)
		{
			var relation = _repository.Find(command.RelationId);

			if (null == relation)
			{
				throw new RelationNotFoundException();
			}

			relation.SetNote(command.NoteId, command.Content);

			_repository.Save(relation, command);
		}
	}
}
