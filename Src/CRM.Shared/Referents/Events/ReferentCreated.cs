using System;
using CRM.EventSourcing;

namespace CRM.Referents.Events
{
	public class ReferentCreated : DomainEvent
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public ReferentCreated(Guid aggregateId, string firstName, string lastName) 
			: base(aggregateId)
		{
			FirstName = firstName;
			LastName = lastName;
		}
	}
}