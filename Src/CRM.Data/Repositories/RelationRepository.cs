using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using CRM.Data.Common;
using CRM.Data.Entities;
using CRM.Relations;
using Newtonsoft.Json;
using ServiceStack.OrmLite;

namespace CRM.Data.Repositories
{
	public class ContractResolver : ValueResolver<RelationContract, string>
	{
		protected override string ResolveCore(RelationContract source)
		{
			return source.Notes != null
				? JsonConvert.SerializeObject(source.Notes)
				: null;
		}
	}

	public class EntityResolver : ValueResolver<RelationEntity, List<RelationNoteContract>>
	{
		protected override List<RelationNoteContract> ResolveCore(RelationEntity source)
		{
			return !string.IsNullOrWhiteSpace(source.NotesJson)
				? JsonConvert.DeserializeObject<List<RelationNoteContract>>(source.NotesJson)
				: new List<RelationNoteContract>();
		}
	}

	public class RelationRepository : IRelationRepository
	{
		private static Expression<Func<RelationEntity, object>> _relationOrder;

		static RelationRepository()
		{
			Mapper.CreateMap<RelationContract, RelationEntity>()
						.ForMember(src => src.NotesJson, opt => opt.ResolveUsing<ContractResolver>());

			Mapper.CreateMap<RelationEntity, RelationContract>()
						.ForMember(src => src.Notes, opt => opt.ResolveUsing<EntityResolver>());

			_relationOrder = relation => new
			                             {
				                             Priority = Sql.Desc(relation.Priority),
				                             relation.ReferentName
			                             };
		}

		private readonly IConnectionFactory _connectionFactory;
		private readonly IGenericRepository<RelationEntity> _repository;

		public RelationRepository(IConnectionFactory connectionFactory, IGenericRepository<RelationEntity> repository )
		{
			_connectionFactory = connectionFactory;
			_repository = repository;
		}

		public RelationContract GetByReferent(Guid referentId)
		{
			using (var connection = _connectionFactory.Create())
			{
				var entity = connection.Single<RelationEntity>(e => e.ReferentId == referentId);

				return Mapper.Map<RelationContract>(entity);
			}
		}

		public RelationContract GetById(Guid id)
		{
			return Mapper.Map<RelationContract>(_repository.GetById(id));
		}

		public IEnumerable<RelationContract> SearchAmongAll(string fragment, Pagination pagination)
		{
			using (var connection = _connectionFactory.Create())
			{
				List<RelationEntity> entities;

				if (string.IsNullOrWhiteSpace(fragment))
				{
					entities = connection.Select<RelationEntity>(x => x.OrderBy(_relationOrder)
						                                        .Skip(pagination.Skip)
						                                        .Take(pagination.Limit));
				}
				else
				{
					entities = connection.Select<RelationEntity>(x => x.Where(e => (e.ClientName.Contains(fragment) ||
					                                                                e.ReferentName.Contains(fragment)))
																														 .OrderBy(_relationOrder)
					                                                   .Skip(pagination.Skip)
					                                                   .Take(pagination.Limit));
				}

				return entities.Select(Mapper.Map<RelationContract>);
			}
		}

		public IEnumerable<RelationContract> SearchAmongFollowed(Guid followerId, string fragment, Pagination pagination)
		{


			using (var connection = _connectionFactory.Create())
			{
				List<RelationEntity> entities;

				if (string.IsNullOrWhiteSpace(fragment))
				{
					entities = connection.Select<RelationEntity>(x => x.Where(e => (followerId == e.OwnerId || followerId == e.PartnerId))
																														 .OrderBy(_relationOrder)
					                                                   .Skip(pagination.Skip)
					                                                   .Take(pagination.Limit));
				}
				else
				{
					entities = connection.Select<RelationEntity>(x => x.Where(e => (followerId == e.OwnerId || followerId == e.PartnerId) &&
					                                                               (e.ClientName.Contains(fragment) ||
					                                                                e.ReferentName.Contains(fragment)))
																														 .OrderBy(_relationOrder)
					                                                   .Skip(pagination.Skip)
					                                                   .Take(pagination.Limit));
				}

				return entities.Select(Mapper.Map<RelationContract>);

			}
		}

		public void Insert(RelationContract relation)
		{
			_repository.Insert(Mapper.Map<RelationEntity>(relation));
		}

		public void Update(RelationContract relation)
		{
			_repository.Update(Mapper.Map<RelationEntity>(relation));

		}

		public void UpdateClientName(Guid clientId, string clientName)
		{
			using (var connection = _connectionFactory.Create())
			{
				connection.Update<RelationEntity>(new {ClientName = clientName},
				                                  p => p.ClientId == clientId);
			}
		}

		public void UpdateReferentName(Guid referentId, string referentName)
		{
			using (var connection = _connectionFactory.Create())
			{
				connection.Update<RelationEntity>(new {ReferentName = referentName},
				                                  p => p.ReferentId == referentId);
			}
		}

		public void Clear()
		{
			_repository.Truncate();
		}
	}
}