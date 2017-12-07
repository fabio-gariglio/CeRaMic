using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Relations.Commands
{
	public class CreateRelationCommand : DomainCommand
	{
		public Guid ReferentId { get; set; }

		public CreateRelationCommand(Guid referentId)
		{
			Condition.Requires(referentId, "referentId").IsNotEqualTo(Guid.Empty);

			ReferentId = referentId;
		}
	}
}
