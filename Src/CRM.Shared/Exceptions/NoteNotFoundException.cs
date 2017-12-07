using System;

namespace CRM.Exceptions
{
	public class NoteNotFoundException : CrmException
	{
		public NoteNotFoundException()
		{
		}

		public NoteNotFoundException(Guid noteId)
			: base(string.Format("No note found for the id [{0}].", noteId))
		{
			NoteId = noteId;
		}

		public NoteNotFoundException(string message)
			: base(message)
		{
		}

		public NoteNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public Guid NoteId { get; set; }
	}
}