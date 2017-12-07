using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Newtonsoft.Json;

namespace CRM.Diagnostic.Elasticsearch
{
	public class ElasticsearchLogListener : ILoggerListener
	{
		private readonly IElasticsearchConfiguration _configuration;

		private readonly Lazy<ElasticsearchClient> _client;

		public ElasticsearchLogListener(IElasticsearchConfiguration configuration)
		{
			_configuration = configuration;
			_client = new Lazy<ElasticsearchClient>(ElasticsearchClientFactory);
		}

		public void Panic(LogMessage message)
		{
			Log(LogSeverity.Panic, message);
		}

		public void Alert(LogMessage message)
		{
			Log(LogSeverity.Alert, message);
		}

		public void Critical(LogMessage message)
		{
			Log(LogSeverity.Critical, message);
		}

		public void Error(LogMessage message)
		{
			Log(LogSeverity.Error, message);
		}

		public void Warning(LogMessage message)
		{
			Log(LogSeverity.Warning, message);
		}

		public void Notice(LogMessage message)
		{
			Log(LogSeverity.Notice, message);
		}

		public void Information(LogMessage message)
		{
			Log(LogSeverity.Information, message);
		}

		public void Debug(LogMessage message)
		{
			Log(LogSeverity.Debug, message);
		}

		private void Log(LogSeverity severity, LogMessage message)
		{
			var body = ElasticLogMessage.FromLogMessage(severity, message);

			_client.Value.Index(_configuration.Index, "log", body);
		}

		private ElasticsearchClient ElasticsearchClientFactory()
		{
			var node = _configuration.Endpoint;
			var config = new ConnectionConfiguration(node);
			var client = new ElasticsearchClient(config);

			return client;
		}
	}

	public class ElasticLogMessage
	{
		public string Severity { get; set; }
		public string MachineName { get; set; }
		public DateTime UtcTimestamp { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public IEnumerable<KeyValuePair<string, object>> Properties { get; set; }

		public static ElasticLogMessage FromLogMessage(LogSeverity severity, LogMessage message)
		{
			return new ElasticLogMessage
			       {
							 Severity = severity.ToString(),
							 MachineName = message.MachineName,
							 UtcTimestamp = message.UtcTimestamp,
							 Content = message.Content,
							 Description = message.Description,
							 Properties = message.Properties
			       };
		}
	}
}