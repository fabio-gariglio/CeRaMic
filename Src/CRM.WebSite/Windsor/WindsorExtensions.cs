using Castle.MicroKernel.Registration;

namespace CRM.WebSite.Windsor
{
	public static class WindsorExtensions
	{
		public static FromAssemblyDescriptor FromBin()
		{
			return Classes.FromAssemblyInDirectory(new AssemblyFilter("bin", "*.dll"));
		}
	}
}