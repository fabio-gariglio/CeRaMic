using System.Collections.Generic;
using ServiceStack.OrmLite;

namespace CRM.Data.Common
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity>, ISingleton
	{
		private readonly IConnectionFactory _connectionFactory;

		public GenericRepository(IConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public void Insert(TEntity entity)
		{
			using (var connection = _connectionFactory.Create())
			{
				connection.Insert(entity);
			}
		}

		public void Update(TEntity entity)
		{
			using (var connection = _connectionFactory.Create())
			{
				connection.Update(entity);
			}
		}

		public TEntity GetById(object id)
		{
			using (var connection = _connectionFactory.Create())
			{
				return connection.SingleById<TEntity>(id);
			}
		}

		public IEnumerable<TEntity> GetAll()
		{
			using (var connection = _connectionFactory.Create())
			{
				return connection.Select<TEntity>();
			}
		}

		public void Remove(object id)
		{
			using (var connection = _connectionFactory.Create())
			{
				connection.DeleteById<TEntity>(id);
			}
		}

		public void Truncate()
		{
			using (var connection = _connectionFactory.Create())
			{
				connection.DeleteAll<TEntity>();
			}
		}
	}
}