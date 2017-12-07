using System;
using CRM.EventSourcing;
using CuttingEdge.Conditions;

namespace CRM.Referents.Events
{
	public class ReferentSecretarySet : DomainEvent
	{
		public string Secretary { get; set; }
		public ReferentSecretarySet(Guid aggregateId, string secretary)
			: base(aggregateId)
		{
			Secretary = secretary;
		}
	}
}