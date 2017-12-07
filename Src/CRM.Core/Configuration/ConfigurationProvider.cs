using System.ComponentModel;
using System.Configuration;
using CRM.Exceptions;

namespace CRM.Configuration
{
	public class ConfigurationProvider : IConfigurationProvider, ISingleton
	{
		public T GetApplicationSetting<T>(string name, T defaultValue)
		{
			var value = defaultValue;
			TryGetApplicationSetting(name, ref value);

			return value;
		}

		public T GetApplicationSetting<T>(string name)
		{
			var value = default(T);
			if (TryGetApplicationSetting(name, ref value))
			{
				return value;
			}

			throw new SettingNotFoundException(string.Format("AppSetting '{0}' not found.", name));
		}

		private static bool TryGetApplicationSetting<T>(string name, ref T value)
		{
			var stringValue = ConfigurationManager.AppSettings[name];

			if (null == stringValue) return false;

			var converter = TypeDescriptor.GetConverter(typeof(T));
			var result = converter.ConvertFrom(stringValue);

			if (null != result) value = (T)result;

			return null != result;
		}

		public string GetConnectionString(string name)
		{
			var connectionString = ConfigurationManager.ConnectionStrings[name];

			return connectionString.ConnectionString;
		}
	}
}
