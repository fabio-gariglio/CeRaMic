using System;
using System.Collections.Generic;

namespace CRM.Utility
{
	public class ExportContract
	{
		public Guid Id { get; set; }

		public IEnumerable<ExportItemContract> Items { get; set; }
	}
}
