namespace CRM.Configuration
{
	public interface IConfigurationProvider
	{
		T GetApplicationSetting<T>(string name, T defaultValue);

		T GetApplicationSetting<T>(string name);

		string GetConnectionString(string name);
	}
}
