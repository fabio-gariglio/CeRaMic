using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace CRM.Common
{
	public class ScriptsProvider : IScriptsProvider, ISingleton
	{
		private readonly IFileSystem _fileSystem;

		public ScriptsProvider(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		public IEnumerable<string> GetScripts(string rootPath)
		{
			var appPath = Path.Combine(rootPath, "app");
			var absolutePaths = _fileSystem.Directory.GetFiles(appPath, "*.js", SearchOption.AllDirectories);
			var relativePaths = absolutePaths.Select(p => p.Replace(rootPath, "/").Replace('\\', '/'));

			return relativePaths.OrderByDescending(GetPathPriority);
		}

		private static int GetPathPriority(string path)
		{
			return path.EndsWith("config.js") ? 1 : 0;
		}
	}
}
