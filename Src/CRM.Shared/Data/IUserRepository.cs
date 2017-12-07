using System;
using System.Collections.Generic;
using CRM.Security;
using CRM.Users;

namespace CRM.Data
{
	public interface IUserRepository : IRepository
	{
		UserContract GetByCredentials(UserCredentials credentials);
		UserContract GetByName(string name);
		UserContract GetByEmail(string email);
		UserContract GetById(Guid id);
		IEnumerable<UserContract> GetAll();
		void Insert(UserContract user);
		void Update(UserContract user);
		void Clear();
	}
}