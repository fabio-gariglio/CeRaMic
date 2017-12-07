using System;
using CRM.Utility;

namespace CRM.Data
{
	public interface IExportRepository : IRepository
	{
		ExportContract GetById(Guid id);
		void Insert(ExportContract export);
		void Clear();
	}
}