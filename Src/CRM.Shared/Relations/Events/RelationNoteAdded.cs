using System;
using CRM.EventSourcing;

namespace CRM.Relations.Events
{
	public class RelationNoteAdded : DomainEvent
	{
		public Guid NoteId { get; set; }
		public string Content { get; set; }

		public RelationNoteAdded(Guid aggregateId, Guid noteId, string content) : base(aggregateId)
		{
			NoteId = noteId;
			Content = content;
		}
	}
}
