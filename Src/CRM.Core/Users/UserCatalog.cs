using System;
using CRM.Data;

namespace CRM.Users
{
	public class UserCatalog : IUserCatalog, ISingleton
	{
		private readonly IUserRepository _userRepository;

		public UserCatalog(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public UserContract this[Guid id]
		{
			get { return _userRepository.GetById(id); }
		}
	}
}
