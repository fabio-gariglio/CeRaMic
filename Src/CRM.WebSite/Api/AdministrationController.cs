using System;
using System.Collections.Generic;
using System.Web.Http;
using CRM.Clients;
using CRM.Data;
using CRM.Users;

namespace CRM.WebSite.Api
{
	[RoutePrefix("crm/api/administration")]
	public class AdministrationController : ApiController
	{
		private readonly IUserRepository _repository;

		public AdministrationController(IUserRepository repository)
		{
			_repository = repository;
		}

		[HttpGet, Route("users"), AuthorizeRoles(UserRoles.Administrator)]
		public IEnumerable<UserContract> GetAll()
		{
			return _repository.GetAll();
		}

	}
}