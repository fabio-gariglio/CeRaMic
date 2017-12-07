using System;
using System.Web.Http;
using CRM.Data;

namespace CRM.WebSite.Api
{
	[RoutePrefix("crm/api/exports")]
	public class ExportController : ApiController
	{
		private readonly IExportRepository _exportRepository;

		public ExportController(IExportRepository exportRepository)
		{
			_exportRepository = exportRepository;
		}


		[HttpGet, Route("export")]
		public object GetAll(Guid id)
		{
			return _exportRepository.GetById(id);
		}
	}
}