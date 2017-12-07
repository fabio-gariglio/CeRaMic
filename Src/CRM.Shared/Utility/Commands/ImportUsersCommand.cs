using CRM.EventSourcing;

namespace CRM.Utility
{
	public class ImportUsersCommand : DomainCommand
	{
		public string UsersJsonContent { get; set; }

		public ImportUsersCommand(string usersJsonContent)
		{
			UsersJsonContent = usersJsonContent;
		}
	}
}
