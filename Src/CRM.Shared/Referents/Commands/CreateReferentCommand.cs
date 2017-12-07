using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Referents.Commands
{
	public class CreateReferentCommand : DomainCommand
	{
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string ClientName { get; private set; }
		public string Area { get; set; }
		public string EmailAddress { get; set; }
		public string MobilePhone { get; set; }
		public string LandlineNumber { get; set; }

		public CreateReferentCommand(string firstName, string lastName, string clientName)
		{
			Condition.Requires(firstName, "firstName").IsNotNullOrWhiteSpace();
			Condition.Requires(clientName, "clientName").IsNotNullOrWhiteSpace();

			FirstName = firstName;
			LastName = lastName;
			ClientName = clientName;
		}
	}
}