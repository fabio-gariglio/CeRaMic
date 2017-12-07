using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Referents.Commands
{
	public class ChangeReferentNameCommand : DomainCommand
	{
		public Guid Id { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; set; }

		public ChangeReferentNameCommand(Guid id, string firstName, string lastName)
		{
			Condition.Requires(id, "id").IsNotEqualTo(Guid.Empty);
			Condition.Requires(firstName, "firstName").IsNotNullOrWhiteSpace();

			Id = id;
			LastName = lastName;
			FirstName = firstName;
		}
	}
}