using System;

namespace CRM.Referents
{
	public interface IReferentAssertion
	{
		void HasUniqueName(string firstName, string lastName, Guid currentId);
	}
}
