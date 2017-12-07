using System;
using CRM.Data;
using CRM.EventSourcing;
using CRM.Utility.Commands;

namespace CRM.Domain.Utility
{
	public class ExportCommandHandler : IDomainCommandHandler<ExportCommand>
	{
		private readonly IExportRepository _exportRepository;
		private readonly IRelationRepository _relationRepository;
		private readonly IReferentRepository _referentRepository;

		public ExportCommandHandler(IExportRepository exportRepository,
		                            IRelationRepository relationRepository,
		                            IReferentRepository referentRepository)
		{
			_exportRepository = exportRepository;
			_relationRepository = relationRepository;
			_referentRepository = referentRepository;
		}

		public void Handle(ExportCommand command)
		{
			throw new NotImplementedException();
		}
	}
}
