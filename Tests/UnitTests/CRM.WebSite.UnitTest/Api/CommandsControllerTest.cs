using CRM.Common;
using CRM.EventSourcing;
using CRM.WebSite.Api;
using Machine.Specifications;
using NSubstitute;

namespace CRM.WebSite.UnitTest.Api
{
	[Subject("Commands controller")]
	public class CommandsControllerTest
	{
		public class FakeCommand : DomainCommand
		{
			public int Value { get; set; }
		}

		private static readonly IDomainCommandBus CommandBus = Substitute.For<IDomainCommandBus>();
		private static readonly ICommandCatalog CommandCatalog = Substitute.For<ICommandCatalog>();
		private static readonly CommandsController Target = new CommandsController(CommandBus, CommandCatalog);

		private Because of = () =>
		                     {
			                     CommandCatalog.GetAll().Returns(new[] {typeof (FakeCommand)});

			                     var command = new CommandContract("FakeCommand", "{Value:10}");
			                     Target.Send(command);
		                     };

		private It should_send_a_FakeCommand_command = () => CommandBus.Received(1).Send(Arg.Any<FakeCommand>());

		private It should_send_a_FakeCommand_commands =
			() => CommandBus.Received(1).Send(Arg.Is<FakeCommand>(cmd => cmd.Value == 10));
	}
}