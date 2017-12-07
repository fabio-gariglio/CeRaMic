using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CRM.Data;
using CRM.Domain.Clients;
using CRM.Domain.Referents;
using CRM.Domain.Relations;
using CRM.Domain.Users;
using CRM.EventSourcing;
using CRM.Exceptions;
using CRM.Extensions;
using CRM.Users;
using CRM.Utility;
using CsvHelper;

namespace CRM.Domain.Utility
{
	public class ImportCsvCommandHandler : IDomainCommandHandler<ImportCsvCommand>
	{
		private readonly Regex _identifierSanitizer = new Regex(@"\s+", RegexOptions.Compiled);

		private readonly IClientRepository _clientRepository;
		private readonly IReferentRepository _referentRepository;
		private readonly IUserRepository _userRepository;
		private readonly IRelationRepository _relationRepository;
		private readonly IAggregateRepository<ClientAggregate> _clientAggregateRepository;
		private readonly IAggregateRepository<ReferentAggregate> _referentAggregateRepository;
		private readonly IAggregateRepository<UserAggregate> _userAggregateRepository;
		private readonly IAggregateRepository<RelationAggregate> _relationAggregateRepository;

		public ImportCsvCommandHandler(IClientRepository clientRepository,
		                               IReferentRepository referentRepository,
		                               IUserRepository userRepository,
		                               IRelationRepository relationRepository,
		                               IAggregateRepository<ClientAggregate> clientAggregateRepository,
		                               IAggregateRepository<ReferentAggregate> referentAggregateRepository,
		                               IAggregateRepository<UserAggregate> userAggregateRepository,
		                               IAggregateRepository<RelationAggregate> relationAggregateRepository)
		{
			_clientRepository = clientRepository;
			_referentRepository = referentRepository;
			_userRepository = userRepository;
			_relationRepository = relationRepository;
			_clientAggregateRepository = clientAggregateRepository;
			_referentAggregateRepository = referentAggregateRepository;
			_userAggregateRepository = userAggregateRepository;
			_relationAggregateRepository = relationAggregateRepository;
		}

		public void Handle(ImportCsvCommand command)
		{
			using (var reader = new StringReader(command.CsvContent))
			{
				using (var csv = new CsvReader(reader))
				{
					while (csv.Read())
					{
						var item = CsvItem.FromCsvReader(csv);

						if (string.IsNullOrEmpty(item.Cliente)) continue;

						var client = GetOrAddClient(item.Cliente);
						if (null != client) _clientAggregateRepository.Save(client, command);

						var referent = GetOrAddReferent(item.Nome, item.Cognome);

						if (null == referent) continue;

						if (null != client) referent.SetClient(client.Id);

						if (null != item.Posizione) referent.SetArea(item.Posizione);

						if (null != item.Segretaria && item.Segretaria != "no") referent.SetSecretary(item.Segretaria);

						if (null != item.Recapito && item.Recapito.Length > 3 && item.Recapito.Trim() != "+39 011")
						{
							switch (item.TipoRecapito)
							{
								case "Email":
									referent.SetEmailContact(item.Recapito);
									break;
								case "Telefono_fisso":
									referent.SetLandlineContact(item.Recapito);
									break;
								case "Telefono_mobile":
									referent.SetMobileContact(item.Recapito);
									break;
							}
						}

						_referentAggregateRepository.Save(referent, command);

						var relation = GetRelation(referent.Id);

						if (null != item.Note && !relation.NoteIds.Any())
						{
							relation.AddNote(item.Note);
						}

						var owner = GetUser(item.Owner);
						var partner = GetUser(item.Partner);

						if (null != owner) relation.SetOwner(owner.Id);
						if (null != partner) relation.SetPartner(partner.Id);

						_relationAggregateRepository.Save(relation, command);

					}
				}
			}
		}

		private UserContract GetUser(string identifier)
		{
			if (string.IsNullOrWhiteSpace(identifier)) return null;

			identifier = _identifierSanitizer.Replace(identifier, " ");

			return _userRepository.GetByEmail(identifier) ?? _userRepository.GetByName(identifier);
		}

		private RelationAggregate GetRelation(Guid referentId)
		{
			var relation = _relationRepository.GetByReferent(referentId);

			if (null == relation)
			{
				throw new RelationNotFoundException();
			}

			return _relationAggregateRepository.Find(relation.Id);
		}

		private ReferentAggregate GetOrAddReferent(string firstName, string lastName)
		{
			var one = StringExtension.BuildFullName(firstName, lastName);
			var two = StringExtension.BuildFullName(lastName, firstName);

			var referent = _referentRepository.GetByName(one) ?? _referentRepository.GetByName(two);

			return null != referent
				? _referentAggregateRepository.Find(referent.Id)
				: new ReferentAggregate(firstName, lastName);
		}

		private ClientAggregate GetOrAddClient(string name)
		{
			var client = _clientRepository.GetByName(name);

			if (null == client)
			{
				return new ClientAggregate(name);
			}

			return _clientAggregateRepository.Find(client.Id);
		}
	}
}