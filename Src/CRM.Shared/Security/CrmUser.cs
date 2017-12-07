using System;
using System.Security.Principal;

namespace CRM.Security
{
	public class CrmUser : IPrincipal
	{
		public Guid Id { get; private set; }
		public string Role { get; private set; }

		public CrmUser(Guid id, string name, string role)
		{
			Id = id;
			Role = role;
			Identity = new CrmIdentity(name);
		}

		public bool IsInRole(string role)
		{
			return Role == role;
		}

		public IIdentity Identity { get; private set; }
	}
}
