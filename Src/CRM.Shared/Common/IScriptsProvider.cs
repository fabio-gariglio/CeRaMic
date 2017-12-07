using System.Collections.Generic;

namespace CRM.Common
{
	public interface IScriptsProvider
	{
		IEnumerable<string> GetScripts(string rootPath);
	}
}
