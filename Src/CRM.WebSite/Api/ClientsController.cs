using System;
using System.Collections.Generic;
using System.Web.Http;
using CRM.Clients;
using CRM.Data;
using CRM.Users;

namespace CRM.WebSite.Api
{
	[RoutePrefix("crm/api/clients")]
	public class ClientsController : ApiController
	{
		private readonly IClientRepository _repository;

		public ClientsController(IClientRepository repository)
		{
			_repository = repository;
		}

		[HttpGet, Route("all"), AuthorizeRoles(UserRoles.Administrator)]
		public IEnumerable<ClientContract> GetAll()
		{
			return _repository.GetAll();
		}

		[HttpGet, Route("id/{clientId}")]
		public ClientContract GetById(Guid clientId)
		{
			return _repository.GetById(clientId);
		}

		[HttpGet, Route("search")]
		public IEnumerable<ClientContract> SearchByName(string name)
		{
			return string.IsNullOrEmpty(name)
				? _repository.GetAll()
				: _repository.SearchByName(name);
		}
	}
}