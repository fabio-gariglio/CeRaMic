using System;
using System.Collections.Generic;
using System.Web.Http;
using CRM.Data;
using CRM.Referents;
using CRM.Users;

namespace CRM.WebSite.Api
{
	[RoutePrefix("crm/api/referents")]
	public class ReferentsController : ApiController
	{
		private readonly IReferentRepository _repository;

		public ReferentsController(IReferentRepository repository)
		{
			_repository = repository;
		}

		[HttpGet, Route("all"), AuthorizeRoles(UserRoles.Administrator)]
		public IEnumerable<ReferentContract> GetAll()
		{
			return _repository.GetAll();
		}

		[HttpGet, Route("id/{referentId}")]
		public ReferentContract GetById(Guid referentId)
		{
			return _repository.GetById(referentId);
		}

		[HttpGet, Route("search"), AuthorizeRoles(UserRoles.Administrator)]
		public IEnumerable<ReferentContract> SearchByName(string name)
		{
			return string.IsNullOrEmpty(name)
				? _repository.GetAll()
				: _repository.SearchByName(name);
		}
	}
}