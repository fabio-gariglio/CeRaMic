using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Referents.Commands
{
	public class ChangeReferentClientCommand : DomainCommand
	{
		public Guid Id { get; private set; }
		public Guid ClientId { get; private set; }
		public ChangeReferentClientCommand(Guid id, Guid clientId)
		{
			Condition.Requires(id, "id").IsNotEqualTo(Guid.Empty);
			Condition.Requires(clientId, "clientId").IsNotEqualTo(Guid.Empty);

			Id = id;
			ClientId = clientId;
		}
	}
}