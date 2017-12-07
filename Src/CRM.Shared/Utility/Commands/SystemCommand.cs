using System;
using CRM.EventSourcing;

namespace CRM.Utility
{
	public class SystemCommand : DomainCommand
	{
		public SystemCommand()
		{
			UserId = Guid.Empty;
			CommandId = Guid.NewGuid();
		}
	}
}
