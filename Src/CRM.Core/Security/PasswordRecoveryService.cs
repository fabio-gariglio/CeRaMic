using System;
using CRM.Data;
using CRM.EventSourcing;
using CRM.Extensions;
using CRM.Users.Commands;
using CuttingEdge.Conditions;

namespace CRM.Security
{
	public class PasswordRecoveryService : IPasswordRecoveryService, ISingleton
	{
		private readonly IUserRepository _userRepository;
		private readonly IDomainCommandBus _commandBus;

		public PasswordRecoveryService(IUserRepository userRepository,
		                               IDomainCommandBus commandBus)
		{
			_userRepository = userRepository;
			_commandBus = commandBus;
		}

		public void Recovery(string email)
		{
			Condition.Requires(email, "email").IsNotNullOrWhiteSpace();

			var user = _userRepository.GetByEmail(email);

			if (null == user) return;

			var command = new RecoveryUserPasswordCommand(user.Id) {CommandId = Guid.NewGuid()};

			_commandBus.Send(command);
		}
	}
}
