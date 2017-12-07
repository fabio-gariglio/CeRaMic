using System;
using System.Data;
using CRM.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace CRM.Data.Common
{
	public class ConnectionFactory : IConnectionFactory, ISingleton
	{
		private readonly IConfigurationProvider _configurationProvider;
		private readonly Lazy<IDbConnectionFactory> _connectionFactory;

		public ConnectionFactory(IConfigurationProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;
			_connectionFactory = new Lazy<IDbConnectionFactory>(CreateConnectionFactory);
		}

		public IDbConnection Create()
		{
			var connection = _connectionFactory.Value.OpenDbConnection();

			return connection;
		}

		private IDbConnectionFactory CreateConnectionFactory()
		{
			var connectionString = _configurationProvider.GetConnectionString("crm");
			var dialectProvider = new SqlServerOrmLiteDialectProvider();
			var connectionFactory = new OrmLiteConnectionFactory(connectionString, dialectProvider);

			return connectionFactory;
		}
	}
}