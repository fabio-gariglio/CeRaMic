using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;

namespace CRM.Diagnostic.Elasticsearch
{
	public class ElasticsearchMonitorListener : IMonitorListener
	{
		private readonly IElasticsearchConfiguration _configuration;

		private readonly Lazy<ElasticsearchClient> _client;

		public ElasticsearchMonitorListener(IElasticsearchConfiguration configuration)
		{
			_configuration = configuration;
			_client = new Lazy<ElasticsearchClient>(ElasticsearchClientFactory);
		}

		public void Trace(MonitorEvent @event)
		{
			_client.Value.Index(_configuration.Index, "monitor", @event);
		}

		private ElasticsearchClient ElasticsearchClientFactory()
		{
			var node = _configuration.Endpoint;
			var config = new ConnectionConfiguration(node);
			var client = new ElasticsearchClient(config);

			return client;
		}
	}
}