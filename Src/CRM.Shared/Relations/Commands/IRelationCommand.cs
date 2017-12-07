using System;
using CRM.EventSourcing;

namespace CRM.Relations.Commands
{
	public interface IRelationCommand : IDomainCommand
	{
		Guid RelationId { get; set; }
	}
}