using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Relations.Commands
{
	public class RejectRelationPartnerCommand : DomainCommand, IRejectRelationCommand, IRelationPartnerCommand
	{
		public Guid RelationId { get; set; }
		public Guid PartnerId { get; set; }

		public RejectRelationPartnerCommand(Guid relationId, Guid partnerId)
		{
			Condition.Requires(relationId, "RelationId").IsNotEqualTo(Guid.Empty);
			Condition.Requires(partnerId, "partnerId").IsNotEqualTo(Guid.Empty);

			RelationId = relationId;
			PartnerId = partnerId;
		}
	}
}