using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CRM.Data.Common;
using CRM.Data.Entities;
using CRM.Referents;
using ServiceStack.OrmLite;

namespace CRM.Data.Repositories
{
	public class ReferentRepository : IReferentRepository
	{
		static ReferentRepository()
		{
			Mapper.CreateMap<ReferentContract, ReferentEntity>();
			Mapper.CreateMap<ReferentEntity, ReferentContract>();
		}

		private readonly IConnectionFactory _connectionFactory;
		private readonly IGenericRepository<ReferentEntity> _repository;

		public ReferentRepository(IConnectionFactory connectionFactory, IGenericRepository<ReferentEntity> repository)
		{
			_connectionFactory = connectionFactory;
			_repository = repository;
		}

		public ReferentContract GetByName(string name)
		{
			using (var connection = _connectionFactory.Create())
			{
				var entity = connection.Single<ReferentEntity>(e => e.Name == name);

				return Mapper.Map<ReferentContract>(entity);
			}
		}

		public IEnumerable<ReferentContract> SearchByName(string name)
		{
			using (var connection = _connectionFactory.Create())
			{
				var entities = connection.Select<ReferentEntity>(e => e.Name.Contains(name));

				return entities.Select(Mapper.Map<ReferentContract>);
			}
		}

		public ReferentContract GetById(Guid id)
		{
			return Mapper.Map<ReferentContract>(_repository.GetById(id));
		}

		public IEnumerable<ReferentContract> GetAll()
		{
			return _repository.GetAll().Select(Mapper.Map<ReferentContract>);
		}

		public void Insert(ReferentContract referent)
		{
			_repository.Insert(Mapper.Map<ReferentEntity>(referent));
		}

		public void Update(ReferentContract referent)
		{
			_repository.Update(Mapper.Map<ReferentEntity>(referent));

		}

		public void Clear()
		{
			_repository.Truncate();
		}
	}
}
