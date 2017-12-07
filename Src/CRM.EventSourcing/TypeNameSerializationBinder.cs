using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace CRM.EventSourcing
{
	public class TypeNameSerializationBinder : SerializationBinder
	{
		private readonly IDictionary<String, Type> _typeNameMapping;

		public TypeNameSerializationBinder(params Type[] baseTypes)
		{
			if (baseTypes == null || baseTypes.Length == 0)
			{
				throw new ArgumentException("You must specify at least one base type.", "baseTypes");
			}

			_typeNameMapping = new Dictionary<String, Type>(StringComparer.InvariantCultureIgnoreCase);

			LoadTypeNameMapping(baseTypes);
		}

		private void LoadTypeNameMapping(Type[] baseTypes)
		{
			foreach (var assembly in GetBinAssemblies())
			{
				foreach (var type in assembly.GetTypes())
				{
					if (!InheritBaseTypes(type, baseTypes))
					{
						continue;
					}

					EnsureTypeNameUniqueness(type);

					_typeNameMapping.Add(type.Name, type);
				}
			}
		}

		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			if (_typeNameMapping.ContainsKey(serializedType.Name))
			{
				assemblyName = null;
				typeName = serializedType.Name;
			}
			else
			{
				assemblyName = serializedType.Assembly.FullName;
				typeName = serializedType.FullName;
			}
		}

		public override Type BindToType(string assemblyName, string typeName)
		{
			if (_typeNameMapping.ContainsKey(typeName))
			{
				return _typeNameMapping[typeName];
			}

			return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName), true);
		}

		private static bool InheritBaseTypes(Type type, IEnumerable<Type> baseTypes)
		{
			return baseTypes.Any(b => b.IsAssignableFrom(type));
		}

		private void EnsureTypeNameUniqueness(Type type)
		{
			if (!_typeNameMapping.ContainsKey(type.Name))
			{
				return;
			}

			var oldType = _typeNameMapping[type.Name];

			var errorMessage = string.Format("Type names must be unique in the assemblies inspected.\nType: {0}\nType: {1}",
																			 oldType.AssemblyQualifiedName,
																			 type.AssemblyQualifiedName);

			throw new InvalidOperationException(errorMessage);
		}

		private static IEnumerable<Assembly> GetBinAssemblies()
		{
			foreach (var file in GetBinFiles())
			{
				if (!IsAssemblyFile(file))
				{
					continue;
				}

				var assembly = LoadAssemblyIgnoringErrors(file);
				if (assembly != null)
				{
					yield return assembly;
				}
			}
		}

		private static Assembly LoadAssemblyIgnoringErrors(string file)
		{
			// based on MEF DirectoryCatalog
			try
			{
				return GetAssemblyNamed(file);
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
				// File was found but could not be loaded
			}
			catch (BadImageFormatException)
			{
				// Dlls that contain native code or assemblies for wrong runtime (like .NET 4 asembly when we're in CLR2 process)
			}
			catch (ReflectionTypeLoadException)
			{
				// Dlls that have missing Managed dependencies are not loaded, but do not invalidate the Directory 
			}
			// TODO: log
			return null;
		}

		public static Assembly GetAssemblyNamed(string filePath)
		{
			var assemblyName = GetAssemblyName(filePath);

			var assembly = Assembly.Load(assemblyName);

			return assembly;
		}

		private static AssemblyName GetAssemblyName(string filePath)
		{
			AssemblyName assemblyName;
			try
			{
				assemblyName = AssemblyName.GetAssemblyName(filePath);
			}
			catch (ArgumentException)
			{
				assemblyName = new AssemblyName { CodeBase = filePath };
			}
			return assemblyName;
		}

		private static bool IsAssemblyFile(string filePath)
		{
			string extension;
			try
			{
				extension = Path.GetExtension(filePath);
			}
			catch (ArgumentException)
			{
				// path contains invalid characters...
				return false;
			}
			return IsDll(extension) || IsExe(extension);
		}

		private static bool IsDll(string extension)
		{
			return ".dll".Equals(extension, StringComparison.OrdinalIgnoreCase);
		}

		private static bool IsExe(string extension)
		{
			return ".exe".Equals(extension, StringComparison.OrdinalIgnoreCase);
		}

		private static IEnumerable<string> GetBinFiles()
		{
			var codeBase = Assembly.GetExecutingAssembly().CodeBase;
			var uri = new UriBuilder(codeBase);
			var fullPath = Uri.UnescapeDataString(uri.Path);
			var path = Path.GetDirectoryName(fullPath);

			return Directory.EnumerateFiles(path);
		}
	}
}
