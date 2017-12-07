using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using AutoMapper;
using CRM.Common;
using CRM.Data.Common;
using CRM.Data.Entities;
using CRM.Security;
using CRM.Users;
using ServiceStack.OrmLite;

namespace CRM.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		static UserRepository()
		{
			Mapper.CreateMap<UserContract, UserEntity>();
			Mapper.CreateMap<UserEntity, UserContract>();
		}

		private readonly IConnectionFactory _connectionFactory;
		private readonly IEncriptionService _encriptionService;
		private readonly IGenericRepository<UserEntity> _repository;

		public UserRepository(IConnectionFactory connectionFactory, 
			IEncriptionService encriptionService,
			IGenericRepository<UserEntity> repository)
		{
			_connectionFactory = connectionFactory;
			_encriptionService = encriptionService;
			_repository = repository;
		}

		public UserContract GetByCredentials(UserCredentials credentials)
		{
			var hashedPassword = _encriptionService.CalculateHash(credentials.Password);

			using (var connection = _connectionFactory.Create())
			{
				var entity = connection
					.Single<UserEntity>(x =>
						                    x.Where(e =>
							                            e.Email == credentials.Email &&
							                            e.PasswordHash == hashedPassword));

				return Mapper.Map<UserContract>(entity);
			}
		}

		public UserContract GetByName(string name)
		{
			using (var connection = _connectionFactory.Create())
			{
				var entity = connection.Single<UserEntity>(x => x.Where(e => e.Name == name));

				return Mapper.Map<UserContract>(entity);
			}
		}

		public UserContract GetByEmail(string email)
		{
			using (var connection = _connectionFactory.Create())
			{
				var entity = connection.Single<UserEntity>(x => x.Where(e => e.Email == email));

				return Mapper.Map<UserContract>(entity);
			}
		}

		public UserContract GetById(Guid id)
		{
			return Mapper.Map<UserContract>(_repository.GetById(id));
		}

		public IEnumerable<UserContract> GetAll()
		{
			return _repository.GetAll().Select(Mapper.Map<UserContract>);
		}
		
		public void Insert(UserContract user)
		{
			_repository.Insert(Mapper.Map<UserEntity>(user));
		}

		public void Update(UserContract user)
		{
			_repository.Update(Mapper.Map<UserEntity>(user));

		}

		public void Clear()
		{
			_repository.Truncate();
		}
	}
}
