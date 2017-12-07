using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.EventSourcing;
using CRM.Exceptions;
using CRM.Relations.Commands;

namespace CRM.Domain.Relations.CommandHandlers
{
	public class UpdateRelationCommandHandler : IDomainCommandHandler<UpdateRelationCommand>
	{
		private readonly IAggregateRepository<RelationAggregate> _repository;

		public UpdateRelationCommandHandler(IAggregateRepository<RelationAggregate> repository)
		{
			_repository = repository;
		}

		public void Handle(UpdateRelationCommand command)
		{
			var relation = _repository.Find(command.RelationId);

			if (null == relation)
			{
				throw new RelationNotFoundException();
			}

			if (command.NoteId.HasValue)
			{
				if (command.NoteId.Value == Guid.Empty)
				{
					relation.AddNote(command.NoteContent);	
				}
				else
				{
					relation.SetNote(command.NoteId.Value, command.NoteContent);	
				}
			}

			if (command.OwnerId.HasValue)
			{
				relation.SetOwner(command.OwnerId.Value);
			}

			if (command.PartnerId.HasValue)
			{
				relation.SetPartner(command.PartnerId.Value);
			}

			relation.ChangePriority(command.Priority);

			_repository.Save(relation, command);
		}
	}
}
