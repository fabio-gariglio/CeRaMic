using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CRM.Data;
using CRM.Users;

namespace CRM.WebSite.Api
{
	[RoutePrefix("crm/api/sellers")]
	public class SellersController : ApiController
	{
		private readonly IUserRepository _repository;

		public SellersController(IUserRepository repository)
		{
			_repository = repository;
		}

		[HttpGet, Route("all")]
		public IEnumerable<SellerDto> GetAll()
		{
			return _repository
				.GetAll()
				.Where(IsSeller)
				.Select(AsSellerDto)
				.OrderBy(x => x.Name);
		}

		private static bool IsSeller(UserContract user)
		{
			return user.Role == UserRoles.Seller;
		}

		private static SellerDto AsSellerDto(UserContract contract)
		{
			return new SellerDto
			       {
							 Id = contract.Id,
							 Name = contract.Name
			       };
		}
	}

	public class SellerDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}