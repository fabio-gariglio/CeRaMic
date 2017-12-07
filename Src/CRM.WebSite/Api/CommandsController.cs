using System;
using System.Linq;
using System.Web.Http;
using CRM.Common;
using CRM.Diagnostic;
using CRM.EventSourcing;
using CRM.Exceptions;
using CRM.Reflection;
using CRM.Security;
using Newtonsoft.Json;

namespace CRM.WebSite.Api
{
	public class CommandContract
	{
		public string Name { get; set; }
		public string Body { get; set; }

		public CommandContract(string name, string body)
		{
			Name = name;
			Body = body;
		}
	}

	[RoutePrefix("crm/api/commands")]
	public class CommandsController : ApiController, INeedLogger
	{
		public ILogger Logger { get; set; }

		private readonly IDomainCommandBus _commandBus;
		private readonly ICommandCatalog _commandCatalog;
		private const StringComparison CompareMode = StringComparison.InvariantCultureIgnoreCase;

		public CommandsController(IDomainCommandBus commandBus, ICommandCatalog commandCatalog)
		{
			_commandBus = commandBus;
			_commandCatalog = commandCatalog;
		}

		[HttpPost, Route("send")]
		public Guid Send(CommandContract commandContract)
		{
			var command = CreateCommand(commandContract);

			Logger.Information(new LogMessage(string.Format("{0} invocation", commandContract.Name)).ByUser());

			_commandBus.Send(command);

			return command.CommandId;
		}

		private DomainCommand CreateCommand(CommandContract commandContract)
		{
			var commandTypes = _commandCatalog.GetAll();
			var commandType = commandTypes.FirstOrDefault(t => String.Equals(t.Name, commandContract.Name, CompareMode));

			if (null == commandType)
			{
				throw new CommandBusException(string.Format("Unknown command '{0}'.", commandContract.Name));
			}

			var command = (DomainCommand)JsonConvert.DeserializeObject(commandContract.Body, commandType);

			if (command.UserId == Guid.Empty)
			{
				command.UserId = ((CrmUser)User).Id;
			}

			return command;
		}
	}
}