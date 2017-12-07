using System;
using System.Linq;
using CRM.Clients.Events;
using CRM.Data;
using CRM.Domain.Referents.Projections;
using CRM.Domain.Utility;
using CRM.EventSourcing;
using CRM.Referents.Events;
using CRM.Relations;
using CRM.Relations.Events;

namespace CRM.Domain.Relations.Projections
{
	[HandlerPriority(100)]
	[HandlerDepencencies(typeof(NamesProjection), typeof(ReferentProjection))]
	public class RelationsProjection : 
		IProjection,
		IDomainEventHandler<RelationCreated>,
		IDomainEventHandler<RelationOwnerApproved>,
		IDomainEventHandler<RelationOwnerRejected>,
		IDomainEventHandler<RelationPartnerApproved>,
		IDomainEventHandler<RelationPartnerRejected>,
		IDomainEventHandler<ReferentClientSet>,
		IDomainEventHandler<ReferentNameChanged>,
		IDomainEventHandler<ClientNameChanged>,
		IDomainEventHandler<RelationNoteAdded>,
		IDomainEventHandler<RelationNoteSet>,
		IDomainEventHandler<RelationPriorityChanged>,
		IDomainEventHandler<ReferentEmailContactSet>,
		IDomainEventHandler<ReferentLandlineContactSet>,
		IDomainEventHandler<ReferentMobileContactSet>
	{
		private readonly IRelationRepository _relationRepository;
		private readonly INameRepository _nameRepository;
		private readonly IReferentRepository _referentRepository;

		public RelationsProjection(IRelationRepository relationRepository, INameRepository nameRepository, IReferentRepository referentRepository)
		{
			_relationRepository = relationRepository;
			_nameRepository = nameRepository;
			_referentRepository = referentRepository;
		}

		public void Handle(RelationCreated @event)
		{
			var referent = _referentRepository.GetById(@event.ReferentId);

			var relation = new RelationContract
			               {
				               Id = @event.AggregateId,
				               ReferentId = referent.Id,
											 ReferentName = referent.Name
			               };

			_relationRepository.Insert(relation);
		}

		public void Handle(ReferentClientSet @event)
		{
			var relation = _relationRepository.GetByReferent(@event.AggregateId);
			var clientName = _nameRepository.GetNameById(@event.ClientId);

			relation.ClientId = @event.ClientId;
			relation.ClientName = clientName;

			_relationRepository.Update(relation);
		}

		public void Handle(RelationOwnerApproved @event)
		{
			var ownerName = _nameRepository.GetNameById(@event.OwnerId);

			UpdateContract(ByRelation,
			               @event.AggregateId,
			               relation =>
			               {
				               relation.OwnerId = @event.OwnerId;
				               relation.OwnerName = ownerName;
			               });
		}

		public void Handle(RelationOwnerRejected @event)
		{
			UpdateContract(ByRelation,
			               @event.AggregateId,
			               relation =>
			               {
				               relation.OwnerId = Guid.Empty;
				               relation.OwnerName = null;
			               });
		}

		public void Handle(RelationPartnerApproved @event)
		{
			var partnerName = _nameRepository.GetNameById(@event.PartnerId);

			UpdateContract(ByRelation,
			               @event.AggregateId,
			               relation =>
			               {
				               relation.PartnerId = @event.PartnerId;
				               relation.PartnerName = partnerName;
			               });
		}

		public void Handle(RelationPartnerRejected @event)
		{
			UpdateContract(ByRelation,
			               @event.AggregateId,
			               relation =>
			               {
				               relation.PartnerId = Guid.Empty;
				               relation.PartnerName = null;
			               });
		}

		public void Handle(RelationNoteAdded @event)
		{
			var note = new RelationNoteContract
			           {
				           Id = @event.NoteId,
				           Content = @event.Content
			           };

			UpdateContract(ByRelation,
			               @event.AggregateId,
			               relation => relation.Notes.Add(note));
		}

		public void Handle(RelationNoteSet @event)
		{
			UpdateContract(ByRelation,
			               @event.AggregateId,
			               relation => relation.Notes.First(n => n.Id == @event.NoteId).Content = @event.Content);
		}

		public void Handle(ClientNameChanged @event)
		{
			var clientName = _nameRepository.GetNameById(@event.AggregateId);

			_relationRepository.UpdateClientName(@event.AggregateId, clientName);
		}

		public void Handle(ReferentNameChanged @event)
		{
			var referentName = _nameRepository.GetNameById(@event.AggregateId);

			_relationRepository.UpdateReferentName(@event.AggregateId, referentName);
		}

		public void Handle(RelationPriorityChanged @event)
		{
			UpdateContract(ByRelation, @event.AggregateId, relation => relation.Priority = @event.Priority);			
		}

		public void Handle(ReferentEmailContactSet @event)
		{
			UpdateContract(ByReferent, @event.AggregateId, relation => relation.EmailAddress = @event.Address);
		}

		public void Handle(ReferentLandlineContactSet @event)
		{
			UpdateContract(ByReferent, @event.AggregateId, relation => relation.LandlineNumber = @event.Number);
		}

		public void Handle(ReferentMobileContactSet @event)
		{
			UpdateContract(ByReferent, @event.AggregateId, relation => relation.MobilePhone = @event.Number);
		}

		public void Truncate()
		{
			_relationRepository.Clear();
		}

		private RelationContract ByRelation(Guid relationId)
		{
			return _relationRepository.GetById(relationId);
		}

		private RelationContract ByReferent(Guid referentId)
		{
			return _relationRepository.GetByReferent(referentId);
		}

		private void UpdateContract(Func<Guid, RelationContract> selector, Guid id, Action<RelationContract> updateAction)
		{
			var contract = selector(id);

			updateAction(contract);

			_relationRepository.Update(contract);
		}
	}
}
