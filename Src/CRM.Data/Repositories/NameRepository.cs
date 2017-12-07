using System;
using AutoMapper;
using CRM.Data.Common;
using CRM.Data.Entities;
using CRM.Utility;

namespace CRM.Data.Repositories
{
	public class NameRepository : INameRepository
	{
		static NameRepository()
		{
			Mapper.CreateMap<NameContract, NameEntity>();
			Mapper.CreateMap<NameEntity, NameContract>();
		}

		private readonly IGenericRepository<NameEntity> _repository;

		public NameRepository(IGenericRepository<NameEntity> repository)
		{
			_repository = repository;
		}

		public string GetNameById(Guid id)
		{
			var entity = _repository.GetById(id);

			return entity != null ? entity.Name : null;
		}

		public void Insert(NameContract name)
		{
			_repository.Insert(Mapper.Map<NameEntity>(name));
		}

		public void Update(NameContract name)
		{
			_repository.Update(Mapper.Map<NameEntity>(name));
		}

		public void Clear()
		{
			_repository.Truncate();
		}
	}
}