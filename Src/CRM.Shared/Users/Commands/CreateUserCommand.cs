using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Users.Commands
{
	public class CreateUserCommand : DomainCommand
	{
		public string Email { get; private set; }
		public string Password { get; private set; }
		public string Name { get; private set; }
		public string Role { get; private set; }
		public CreateUserCommand(string email, string password, string name, string role)
		{
			Condition.Requires(email, "email").IsNotNullOrEmpty();
			Condition.Requires(password, "password").IsNotNullOrEmpty();
			Condition.Requires(name, "name").IsNotNullOrEmpty();
			Condition.Requires(role, "role").IsNotNullOrEmpty();

			Email = email;
			Password = password;
			Name = name;
			Role = role;
		}
	}
}
