using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Domain.Users;
using CRM.EventSourcing;
using CRM.Extensions;
using CRM.Utility;

namespace CRM.Domain.Utility
{
	public class ImportUsersCommandHandler : IDomainCommandHandler<ImportUsersCommand>
	{
		private readonly IUserRepository _userRepository;
		private readonly IAggregateRepository<UserAggregate> _aggregateRepository;

		public ImportUsersCommandHandler(IUserRepository userRepository, 
			IAggregateRepository<UserAggregate> aggregateRepository)
		{
			_userRepository = userRepository;
			_aggregateRepository = aggregateRepository;
		}


		public class UserImportDto
		{
			public string Email { get; set; }
			public string Password { get; set; }
			public string Name { get; set; }
			public string Role { get; set; }
		}

		public void Handle(ImportUsersCommand command)
		{
			var usersToImport = command.UsersJsonContent.To<List<UserImportDto>>();

			usersToImport.ForEach(u => ImportUser(u, command));
		}

		private void ImportUser(UserImportDto userImport, IDomainCommand command)
		{
			var user = _userRepository.GetByEmail(userImport.Email);

			if (user != null) return;

			var aggregate = new UserAggregate(userImport.Email, userImport.Password, userImport.Name, userImport.Role);

			_aggregateRepository.Save(aggregate, command);
		}
	}
}
