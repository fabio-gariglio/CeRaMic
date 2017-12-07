using System;
using CRM.Utility;

namespace CRM.Data
{
	public interface INameRepository : IRepository
	{
		string GetNameById(Guid id);
		void Insert(NameContract name);
		void Update(NameContract name);
		void Clear();
	}
}