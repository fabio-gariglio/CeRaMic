using System;
using CRM.Data;
using CRM.Exceptions;
using CRM.Extensions;

namespace CRM.Referents
{
	public class ReferentAssertion : IReferentAssertion, ISingleton
	{
		private readonly IReferentRepository _repository;

		public ReferentAssertion(IReferentRepository repository)
		{
			_repository = repository;
		}

		public void HasUniqueName(string firstName, string lastName, Guid currentId)
		{
			var name = StringExtension.BuildFullName(firstName, lastName);

			var referent = _repository.GetByName(name);

			if (null == referent)
			{
				return;
			}

			if (referent.Id == currentId)
			{
				return;
			}

			throw new DuplicatedReferentException(name);
		}
	}
}
