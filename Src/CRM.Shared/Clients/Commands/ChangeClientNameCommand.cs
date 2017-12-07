using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Clients.Commands
{
	public class ChangeClientNameCommand : DomainCommand
	{
		public Guid Id { get; private set; }
		public string Name { get; set; }

		public ChangeClientNameCommand(Guid id, string name)
		{
			Condition.Requires(id, "id").IsNotEqualTo(Guid.Empty);
			Condition.Requires(name, "name").IsNotNullOrWhiteSpace();

			Id = id;
			Name = name;
		}
	}
}