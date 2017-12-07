using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Relations.Commands
{
	public class ApproveRelationOwnerCommand : DomainCommand, IApproveRelationCommand, IRelationOwnerCommand
	{
		public Guid RelationId { get; set; }
		public Guid OwnerId { get; set; }

		public ApproveRelationOwnerCommand(Guid relationId, Guid ownerId)
		{
			Condition.Requires(relationId, "relationId").IsNotEqualTo(Guid.Empty);
			Condition.Requires(ownerId, "ownerId").IsNotEqualTo(Guid.Empty);

			RelationId = relationId;
			OwnerId = ownerId;
		}
	}
}