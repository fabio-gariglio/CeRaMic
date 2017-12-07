using System;
using System.Collections.Generic;
using CRM.EventSourcing;

namespace CRM.Common
{
	public class CommandCatalog : ICommandCatalog, ISingleton
	{
		private IEnumerable<Type> _commandTypes;

		public IEnumerable<Type> GetAll()
		{
			if (null == _commandTypes)
			{
				var result = new List<Type>();

				foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (var type in assembly.GetTypes())
					{
						if(!typeof(IDomainCommand).IsAssignableFrom(type)) continue;

						result.Add(type);
					}
				}

				_commandTypes = result;
			}

			return _commandTypes;
		}
	}
}
