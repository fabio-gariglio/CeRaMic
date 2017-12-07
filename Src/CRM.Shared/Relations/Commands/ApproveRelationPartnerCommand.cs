using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Relations.Commands
{
	public class ApproveRelationPartnerCommand : DomainCommand, IApproveRelationCommand, IRelationPartnerCommand
	{
		public Guid RelationId { get; set; }
		public Guid PartnerId { get; set; }

		public ApproveRelationPartnerCommand(Guid relationId, Guid partnerId)
		{
			Condition.Requires(relationId, "relationId").IsNotEqualTo(Guid.Empty);
			Condition.Requires(partnerId, "partnerId").IsNotEqualTo(Guid.Empty);

			RelationId = relationId;
			PartnerId = partnerId;
		}
	}
}