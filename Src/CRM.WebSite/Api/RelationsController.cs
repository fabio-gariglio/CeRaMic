using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Data;
using CRM.Relations;
using CRM.Security;
using CRM.Users;

namespace CRM.WebSite.Api
{
	[RoutePrefix("crm/api/relations")]
	public class RelationsController : ApiController
	{
		private readonly IRelationRepository _relationRepository;

		public RelationsController(IRelationRepository relationRepository)
		{
			_relationRepository = relationRepository;
		}

		[HttpGet, Route("search")]
		public IEnumerable<RelationDto> Search(int limit, int skip, string fragment = null)
		{
			var user = RequestContext.Principal as CrmUser;

			if (null == user) throw new HttpRequestException();

			var pagination = new Pagination(limit, skip);

			IEnumerable<RelationContract> relations;

			var userId = user.Id;

			switch (user.Role)
			{
				case UserRoles.Seller:
					relations = _relationRepository.SearchAmongFollowed(userId, fragment, pagination);
					break;
				case UserRoles.Supervisor:
					relations = _relationRepository.SearchAmongAll(fragment, pagination);
					break;
				default:
					throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			return relations.Select(c => AsRelationDto(c, userId)).ToList();
		}

		[HttpGet, Route("id/{relationId}")]
		public RelationDetailDto GetRelation(Guid relationId)
		{
			var user = RequestContext.Principal as CrmUser;

			if (null == user) throw new HttpRequestException();

			var relation = _relationRepository.GetById(relationId);

			if (relation == null) throw new HttpRequestException();

			if (user.Role != UserRoles.Seller) return AsRelationDetailDto(relation);

			if (relation.OwnerId != user.Id &&
			    relation.PartnerId != user.Id)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			return AsRelationDetailDto(relation);
		}

		private static RelationDto AsRelationDto(RelationContract contract, Guid userId)
		{
			var dto = new RelationDto
			          {
									Id = contract.Id,
				          ReferentId = contract.ReferentId,
				          Type = GetRelationType(contract, userId),
									ReferentName = contract.ReferentName,
									ClientName = contract.ClientName,
									OwnerName = contract.OwnerName,
									PartnerName = contract.PartnerName,
									EmailAddress = contract.EmailAddress,
									LandlineNumber = contract.LandlineNumber,
									MobilePhone = contract.MobilePhone,
									Priority = contract.Priority
			          };

			return dto;
		}

		private static RelationDetailDto AsRelationDetailDto(RelationContract contract)
		{
			var dto = new RelationDetailDto
			          {
				          Id = contract.Id,
				          ReferentId = contract.ReferentId,
				          ReferentName = contract.ReferentName,
				          ClientName = contract.ClientName,
				          OwnerId = contract.OwnerId,
				          PartnerId = contract.PartnerId,
									Priority = contract.Priority
			          };

			if (contract.Notes.Any())
			{
				var note = contract.Notes.First();

				dto.NoteId = note.Id;
				dto.NoteContent = note.Content;

			}

			return dto;
		}

		private static RelationType GetRelationType(RelationContract contract, Guid userId)
		{
			if (contract.OwnerId == userId) return RelationType.Ownership;
			if (contract.PartnerId == userId) return RelationType.Partnership;

			return RelationType.None;
		}
	}

	public enum RelationType
	{
		None = 0,
		Ownership = 1,
		Partnership = 2
	}

	public class RelationDto
	{
		public Guid Id { get; set; }
		public Guid ReferentId { get; set; }
		public string ReferentName { get; set; }
		public string ClientName { get; set; }
		public RelationType Type { get; set; }
		public string OwnerName { get; set; }
		public string PartnerName { get; set; }
		public string EmailAddress { get; set; }
		public string LandlineNumber { get; set; }
		public string MobilePhone { get; set; }
		public int Priority { get; set; }
	}

	public class RelationDetailDto
	{
		public Guid Id { get; set; }
		public Guid ReferentId { get; set; }
		public Guid OwnerId { get; set; }
		public Guid PartnerId { get; set; }
		public string ReferentName { get; set; }
		public string ClientName { get; set; }
		public Guid NoteId { get; set; }
		public string NoteContent { get; set; }
		public int Priority { get; set; }
	}

}