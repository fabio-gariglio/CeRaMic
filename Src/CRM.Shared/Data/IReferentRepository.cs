using System;
using System.Collections.Generic;
using CRM.Referents;

namespace CRM.Data
{
	public interface IReferentRepository : IRepository
	{
		ReferentContract GetByName(string name);
		IEnumerable<ReferentContract> SearchByName(string name);
		ReferentContract GetById(Guid id);
		IEnumerable<ReferentContract> GetAll();
		void Insert(ReferentContract referent);
		void Update(ReferentContract referent);
		void Clear();
	}
}