using System;
using CRM.Configuration;

namespace CRM.Diagnostic.Elasticsearch
{
	public class ElasticsearchConfiguration : IElasticsearchConfiguration, ISingleton
	{
		private readonly IConfigurationProvider _configurationProvider;
		private readonly Lazy<Uri> _endpoint;
		private readonly Lazy<string> _index;

		public Uri Endpoint
		{
			get { return _endpoint.Value; }
		}

		public string Index
		{
			get { return _index.Value; }
		}


		public ElasticsearchConfiguration(IConfigurationProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;

			_endpoint = new Lazy<Uri>(EndpointUriFactory);
			_index = new Lazy<string>(IndexFactory);
		}

		private Uri EndpointUriFactory()
		{
			var connectionString = _configurationProvider.GetConnectionString("elasticsearch");

			return new Uri(connectionString);
		}

		private string IndexFactory()
		{
			return _configurationProvider.GetApplicationSetting("elasticSearchIndex", "crm");
		}
	}
}