using System;

namespace CRM.Relations.Commands
{
	public interface IRelationOwnerCommand : IRelationCommand
	{
		Guid OwnerId { get; set; }
	}
}