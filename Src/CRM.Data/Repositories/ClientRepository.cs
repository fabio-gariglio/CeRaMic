using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CRM.Clients;
using CRM.Data.Common;
using CRM.Data.Entities;
using ServiceStack.OrmLite;

namespace CRM.Data.Repositories
{
	public class ClientRepository : IClientRepository
	{
		static ClientRepository()
		{
			Mapper.CreateMap<ClientContract, ClientEntity>();
			Mapper.CreateMap<ClientEntity, ClientContract>();
		}

		private readonly IConnectionFactory _connectionFactory;
		private readonly IGenericRepository<ClientEntity> _repository;

		public ClientRepository(IConnectionFactory connectionFactory, IGenericRepository<ClientEntity> repository)
		{
			_connectionFactory = connectionFactory;
			_repository = repository;
		}

		public ClientContract GetByName(string name)
		{
			using (var connection = _connectionFactory.Create())
			{
				var entity = connection.Single<ClientEntity>(e => e.Name == name);

				return Mapper.Map<ClientContract>(entity);
			}
		}

		public IEnumerable<ClientContract> SearchByName(string name)
		{
			using (var connection = _connectionFactory.Create())
			{
				var entities = connection.Select<ClientEntity>(e => e.Name.Contains(name));

				return entities.Select(Mapper.Map<ClientContract>);
			}
		}

		public ClientContract GetById(Guid id)
		{
			return Mapper.Map<ClientContract>(_repository.GetById(id));
		}

		public IEnumerable<ClientContract> GetAll()
		{
			return _repository.GetAll().Select(Mapper.Map<ClientContract>);
		}

		public void Insert(ClientContract relation)
		{
			_repository.Insert(Mapper.Map<ClientEntity>(relation));
		}

		public void Update(ClientContract relation)
		{
			_repository.Update(Mapper.Map<ClientEntity>(relation));

		}

		public void Clear()
		{
			_repository.Truncate();
		}
	}
}