using System.Collections.Generic;

namespace CRM.Data.Common
{
	public interface IGenericRepository<TEntity>
	{
		void Insert(TEntity entity);
		void Update(TEntity entity);
		TEntity GetById(object id);
		IEnumerable<TEntity> GetAll();
		void Remove(object id);
		void Truncate();
	}
}