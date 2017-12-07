using System;
using CRM.EventSourcing;

namespace CRM.Relations.Commands
{
	public class UpdateRelationCommand : DomainCommand
	{
		public Guid RelationId { get; set; }
		public Guid? OwnerId { get; set; }
		public Guid? PartnerId { get; set; }
		public Guid? NoteId { get; set; }
		public string NoteContent { get; set; }
		public int Priority { get; set; }

		public UpdateRelationCommand(Guid relationId)
		{
			RelationId = relationId;
		}
	}
}
