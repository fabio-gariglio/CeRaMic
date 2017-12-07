using System;

namespace CRM.EventSourcing
{
	public class HandlerPriorityAttribute : Attribute
	{
		public int Priority { get; set; }

		public HandlerPriorityAttribute(int priority)
		{
			Priority = priority;
		}
	}
}
