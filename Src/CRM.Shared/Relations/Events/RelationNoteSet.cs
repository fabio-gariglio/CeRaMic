using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationNoteSet : DomainEvent
	{
		public Guid NoteId { get; set; }
		public string Content { get; set; }

		public RelationNoteSet(Guid aggregateId, Guid noteId, string content)
			: base(aggregateId)
		{
			NoteId = noteId;
			Content = content;
		}
	}
}
