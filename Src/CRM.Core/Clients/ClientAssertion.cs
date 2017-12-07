using CRM.Data;
using CRM.Exceptions;

namespace CRM.Clients
{
	public class ClientAssertion : IClientAssertion, ISingleton
	{
		private readonly IClientRepository _clientRepository;

		public ClientAssertion(IClientRepository clientRepository)
		{
			_clientRepository = clientRepository;
		}

		public void HasUniqueName(string name)
		{
			if (null == _clientRepository.GetByName(name))
			{
				return;
			}

			throw new DuplicatedClientException { Name = name };
		}
	}
}
