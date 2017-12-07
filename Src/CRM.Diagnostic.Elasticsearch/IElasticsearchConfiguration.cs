using System;

namespace CRM.Diagnostic.Elasticsearch
{
	public interface IElasticsearchConfiguration
	{
		Uri Endpoint { get; }

		string Index { get; }
	}
}