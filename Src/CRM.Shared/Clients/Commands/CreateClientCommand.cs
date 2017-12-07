using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Clients.Commands
{
	public class CreateClientCommand : DomainCommand
	{
		public string Name { get; private set; }

		public CreateClientCommand(string name)
		{
			Condition.Requires(name, "name").IsNotNullOrWhiteSpace();

			Name = name;
		}
	}
}
