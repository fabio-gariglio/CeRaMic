using System;

namespace CRM.Users
{
	public interface IUserCatalog
	{
		UserContract this[Guid id] { get; }
	}
}
