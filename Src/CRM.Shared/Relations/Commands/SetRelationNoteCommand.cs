using System;
using CRM.EventSourcing;

namespace CRM.Relations.Commands
{
	public class SetRelationNoteCommand : DomainCommand
	{
		public Guid RelationId { get; set; }
		public Guid NoteId { get; set; }
		public string Content { get; set; }

		public SetRelationNoteCommand(Guid relationId, Guid noteId, string content)
		{
			RelationId = relationId;
			NoteId = noteId;
			Content = content;
		}
	}
}
