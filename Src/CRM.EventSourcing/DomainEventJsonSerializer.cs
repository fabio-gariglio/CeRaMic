using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NEventStore;
using NEventStore.Serialization;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace CRM.EventSourcing
{
	public class DomainEventJsonSerializer : ISerialize
	{
		private readonly IEnumerable<Type> _knownTypes = new[]
		                                                 {
			                                                 typeof (List<EventMessage>),
			                                                 typeof (Dictionary<string, object>)
		                                                 };

		private readonly JsonSerializer _typedSerializer;
		private readonly JsonSerializer _untypedSerializer;

		public DomainEventJsonSerializer()
		{
			var binder = new TypeNameSerializationBinder(typeof(IDomainEvent));

			_typedSerializer = new JsonSerializer
			{
				TypeNameHandling = TypeNameHandling.All,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				NullValueHandling = NullValueHandling.Ignore,
				Binder = binder
			};

			_untypedSerializer = new JsonSerializer
			{
				TypeNameHandling = TypeNameHandling.Auto,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				NullValueHandling = NullValueHandling.Ignore,
				Binder = binder
			};
		}

		public virtual void Serialize<T>(Stream output, T graph)
		{
			using (var streamWriter = new StreamWriter(output, Encoding.UTF8))
				Serialize(new JsonTextWriter(streamWriter), graph);
		}

		public virtual T Deserialize<T>(Stream input)
		{
			using (var streamReader = new StreamReader(input, Encoding.UTF8))
				return Deserialize<T>(new JsonTextReader(streamReader));
		}

		protected virtual void Serialize(JsonWriter writer, object graph)
		{
			using (writer)
				GetSerializer(graph.GetType()).Serialize(writer, graph);
		}

		protected virtual T Deserialize<T>(JsonReader reader)
		{
			Type type = typeof(T);

			using (reader)
				return (T)GetSerializer(type).Deserialize(reader, type);
		}

		protected virtual JsonSerializer GetSerializer(Type typeToSerialize)
		{
			return _knownTypes.Contains(typeToSerialize)
				? _untypedSerializer
				: _typedSerializer;
		}
	}
}