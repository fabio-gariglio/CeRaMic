using System;
using System.Collections.Generic;
using CRM.Relations;

namespace CRM.Data
{
	public interface IRelationRepository : IRepository
	{
		RelationContract GetByReferent(Guid referentId);
		RelationContract GetById(Guid id);
		IEnumerable<RelationContract> SearchAmongAll(string fragment, Pagination pagination);
		IEnumerable<RelationContract> SearchAmongFollowed(Guid followerId, string name, Pagination pagination);
		void UpdateClientName(Guid clientId, string clientName);
		void UpdateReferentName(Guid referentId, string referentName);
		void Insert(RelationContract relation);
		void Update(RelationContract relation);
		void Clear();
	}
}