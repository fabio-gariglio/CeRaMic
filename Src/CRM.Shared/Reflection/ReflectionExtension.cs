using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CRM.Reflection
{
	public static class ReflectionExtension
	{
		public static void Invoke(this object target, string methodName, params object[] parameters)
		{
			var method = target.GetType()
												 .GetMethods(BindingFlags.Instance |
																		 BindingFlags.Public |
																		 BindingFlags.NonPublic |
																		 BindingFlags.FlattenHierarchy)
												 .Where(m => m.Name == methodName)
												 .FirstOrDefault(m => MatchSignature(m, parameters));

			if (method == null)
			{
				return;
			}

			method.Invoke(target, parameters);
		}

		private static bool MatchSignature(MethodInfo method, params object[] parameters)
		{
			var parameterInfos = method.GetParameters();

			if (parameters.Length > parameterInfos.Length)
			{
				return false;
			}

			var parameterIndex = 0;

			for (parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
			{
				var parameter = parameters[parameterIndex];

				if (null == parameter)
				{
					continue;
				}

				if (!parameterInfos[parameterIndex].ParameterType.IsInstanceOfType(parameter))
				{
					return false;
				}
			}

			for (var i = parameterIndex; i < parameterInfos.Length; i++)
			{
				if (!parameterInfos[i].HasDefaultValue)
				{
					return false;
				}
			}

			return true;
		}

		public static bool IsConcrete(Type type)
		{
			return !type.IsAbstract
			       && !type.IsInterface;
		}

		public static IEnumerable<Type> GetHierarchy(this Type type)
		{
			var result = new List<Type>();

			var currentType = type;

			while (null != currentType)
			{
				result.Add(currentType);

				currentType = currentType.BaseType;
			}

			result.AddRange(type.GetInterfaces());

			return result;
		}
	}
}
