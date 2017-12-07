using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Relations.Commands
{
	public class SuggestRelationOwnerCommand : DomainCommand, ISuggestRelationCommand, IRelationOwnerCommand
	{
		public Guid RelationId { get; set; }
		public Guid OwnerId { get; set; }

		public SuggestRelationOwnerCommand(Guid relationId, Guid ownerId)
		{
			Condition.Requires(relationId, "RelationId").IsNotEqualTo(Guid.Empty);
			Condition.Requires(ownerId, "ownerId").IsNotEqualTo(Guid.Empty);

			RelationId = relationId;
			OwnerId = ownerId;
		}
	}
}
