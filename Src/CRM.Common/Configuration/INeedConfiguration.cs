using CRM.Configuration;

namespace CRM.Diagnostic
{
	public interface INeedConfiguration
	{
		IConfigurationProvider ConfigurationProvider { get; set; }
	}
}
