using System;

namespace CRM.EventSourcing
{
	public class HandlerDepencenciesAttribute : Attribute
	{
		public Type[] Dependencies { get; private set; }

		public HandlerDepencenciesAttribute(params Type[] dependencies)
		{
			Dependencies = dependencies;
		}
	}
}
