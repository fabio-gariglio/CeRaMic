using System;

namespace CRM.Relations.Commands
{
	public interface IRelationPartnerCommand : IRelationCommand
	{
		Guid PartnerId { get; set; }
	}
}