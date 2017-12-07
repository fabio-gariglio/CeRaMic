using System;

namespace CRM.Relations.Events
{
	public interface IRelationPartnerEvent : IRelationEvent
	{
		Guid PartnerId { get; set; }
	}
}