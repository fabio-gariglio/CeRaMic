using System;
using CRM.EventSourcing;

namespace CRM.Utility.Commands
{
	public class ExportCommand : DomainCommand
	{
		public Guid ExportId { get; set; }
		public Guid[] SellerIds { get; set; }

		public ExportCommand(Guid exportId)
		{
			ExportId = exportId;
		}
	}
}
