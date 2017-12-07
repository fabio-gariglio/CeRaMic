using System;
using CuttingEdge.Conditions;

namespace CRM.Referents.Commands
{
	public class UpdateReferentCommand : CreateReferentCommand
	{
		public Guid Id { get; private set; }

		public UpdateReferentCommand(Guid id, string firstName, string lastName, string clientName)
			: base(firstName, lastName, clientName)
		{
			Condition.Requires(id, "id").IsNotEqualTo(Guid.Empty);

			Id = id;
		}
	}
}
