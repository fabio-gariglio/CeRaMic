using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CRM.Configuration;
using NEventStore.Persistence.Sql;

namespace CRM.EventSourcing
{
	public class SqlConnectionFactory : IConnectionFactory, ISingleton
	{
		private readonly IConfigurationProvider _configurationProvider;

		public SqlConnectionFactory(IConfigurationProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;
		}

		public IDbConnection Open()
		{
			var connectionString = _configurationProvider.GetConnectionString("crm");
			var connection = new SqlConnection(connectionString);
			
			connection.Open();

			return connection;
		}

		public Type GetDbProviderFactoryType()
		{
			return DbProviderFactories.GetFactory("System.Data.SqlClient").GetType();
		}
	}
}
