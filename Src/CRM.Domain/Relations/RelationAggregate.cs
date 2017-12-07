using System;
using System.Collections.Generic;
using CRM.EventSourcing;
using CRM.Exceptions;
using CRM.Relations.Events;
using CuttingEdge.Conditions;

namespace CRM.Domain.Relations
{
	public class RelationAggregate : AggregateRootBase
	{
		private Guid _ownerId;
		private Guid _partnerId;
		private int _priority;
		public readonly List<Guid> NoteIds = new List<Guid>();

		public RelationAggregate() { }

		public RelationAggregate(Guid referentId)
		{
			ApplyChange(new RelationCreated(Guid.NewGuid(), referentId));
		}

		public void SetOwner(Guid ownerId)
		{
			if(_ownerId == ownerId) return;

			if (_ownerId != Guid.Empty)
			{
				ApplyChange(new RelationOwnerRejected(Id, _ownerId));
			}

			if (ownerId != Guid.Empty)
			{
				ApplyChange(new RelationOwnerApproved(Id, ownerId));
			}
		}

		public void SetPartner(Guid partnerId)
		{
			if (_partnerId == partnerId) return;

			if (_partnerId != Guid.Empty)
			{
				ApplyChange(new RelationPartnerRejected(Id, _partnerId));
			}

			if (partnerId != Guid.Empty)
			{
				ApplyChange(new RelationPartnerApproved(Id, partnerId));
			}
		}

		public void AddNote(string content)
		{
			ApplyChange(new RelationNoteAdded(Id, Guid.NewGuid(), content));
		}

		public void SetNote(Guid noteId, string content)
		{
			Condition.Requires(noteId, "noteId").IsNotEqualTo(Guid.Empty);

			if (!NoteIds.Contains(noteId))
			{
				throw new NoteNotFoundException(noteId);
			}

			ApplyChange(new RelationNoteSet(Id, noteId, content));
		}

		public void ChangePriority(int priority)
		{
			Condition.Requires(priority, "priority").IsGreaterOrEqual(0);

			if(_priority == priority) return;
			
			ApplyChange(new RelationPriorityChanged(Id, priority));
		}

		#region Events Handling

		private void Apply(RelationCreated @event)
		{
			Id = @event.AggregateId;
		}

		private void Apply(RelationOwnerApproved @event)
		{
			_ownerId = @event.OwnerId;
		}

		private void Apply(RelationOwnerRejected @event)
		{
			_ownerId = Guid.Empty;
		}

		private void Apply(RelationPartnerApproved @event)
		{
			_partnerId = @event.PartnerId;
		}

		private void Apply(RelationPartnerRejected @event)
		{
			_partnerId = Guid.Empty;
		}

		private void Apply(RelationNoteAdded @event)
		{
			NoteIds.Add(@event.NoteId);
		}

		private void Apply(RelationPriorityChanged @event)
		{
			_priority = @event.Priority;
		}

		#endregion

	}
}
