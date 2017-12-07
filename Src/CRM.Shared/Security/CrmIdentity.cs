using System.Security.Principal;

namespace CRM.Security
{
	public class CrmIdentity : IIdentity
	{
		public CrmIdentity(string name)
		{
			Name = name;
			IsAuthenticated = true;
		}

		public string Name { get; private set; }
		public string AuthenticationType { get { return "Form"; } }
		public bool IsAuthenticated { get; private set; }
	}
}