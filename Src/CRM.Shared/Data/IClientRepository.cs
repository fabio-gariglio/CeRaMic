using System;
using System.Collections.Generic;
using CRM.Clients;

namespace CRM.Data
{
	public interface IClientRepository : IRepository
	{
		ClientContract GetByName(string name);
		IEnumerable<ClientContract> SearchByName(string name);
		ClientContract GetById(Guid id);
		IEnumerable<ClientContract> GetAll();
		void Insert(ClientContract client);
		void Update(ClientContract client);
		void Clear();
	}
}