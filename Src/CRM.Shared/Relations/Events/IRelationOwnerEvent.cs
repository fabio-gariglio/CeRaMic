using System;

namespace CRM.Relations.Events
{
	public interface IRelationOwnerEvent : IRelationEvent
	{
		Guid OwnerId { get; set; }
	}
}