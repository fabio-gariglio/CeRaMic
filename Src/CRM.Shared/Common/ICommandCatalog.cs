using System;
using System.Collections.Generic;

namespace CRM.Common
{
	public interface ICommandCatalog
	{
		IEnumerable<Type> GetAll();
	}
}
