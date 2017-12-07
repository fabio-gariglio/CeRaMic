using System;
using System.Text.RegularExpressions;
using CRM.EventSourcing;
using CRM.Referents.Events;
using CuttingEdge.Conditions;

namespace CRM.Domain.Referents
{
	public class ReferentAggregate : AggregateRootBase
	{
		private const StringComparison CompareMode = StringComparison.InvariantCultureIgnoreCase;

		#region Public properties

		public Guid ClientId { get; set; }
		public string Area { get; set; }
		public string Secretary { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string EmailAddress { get; set; }
		public string MobileNumber { get; set; }
		public string LandlineNumber { get; set; }

		#endregion

		public ReferentAggregate(){}

		public ReferentAggregate(string firstName, string lastName)
		{
			Condition.Requires(firstName, "firstName").IsNotNullOrWhiteSpace();

			ApplyChange(new ReferentCreated(Guid.NewGuid(), SanitizeString(firstName), SanitizeString(lastName)));
		}

		public void ChangeName(string firstName, string lastName)
		{
			Condition.Requires(firstName, "firstName").IsNotNullOrWhiteSpace();

			if (string.Equals(firstName, FirstName, CompareMode) &&
					string.Equals(lastName, LastName, CompareMode))
			{
				return;
			}

			ApplyChange(new ReferentNameChanged(Id, SanitizeString(firstName), SanitizeString(lastName)));
		}

		public void SetClient(Guid clientId)
		{
			Condition.Requires(clientId, "clientId").IsNotEqualTo(Guid.Empty);

			if (clientId == ClientId) return;

			ApplyChange(new ReferentClientSet(Id, clientId));
		}

		public void SetArea(string area)
		{
			if (string.Equals(area, Area, CompareMode)) return;

			ApplyChange(new ReferentAreaSet(Id, SanitizeString(area)));
		}

		public void SetEmailContact(string address)
		{
			if (string.Equals(address, EmailAddress, CompareMode)) return;

			ApplyChange(new ReferentEmailContactSet(Id, SanitizeString(address)));
		}

		public void SetLandlineContact(string number)
		{
			var sanitizedNumber = SanitizeLandlineNumber(number);

			if (string.Equals(sanitizedNumber, LandlineNumber, CompareMode)) return;

			ApplyChange(new ReferentLandlineContactSet(Id, sanitizedNumber));
		}

		public void SetMobileContact(string number)
		{
			var sanitizedNumber = SanitizeMobilePhone(number);

			if (string.Equals(sanitizedNumber, MobileNumber, CompareMode)) return;

			ApplyChange(new ReferentMobileContactSet(Id, sanitizedNumber));
		}

		public void SetSecretary(string secretary)
		{
			if (string.Equals(secretary, Secretary, CompareMode)) return;
			
			ApplyChange(new ReferentSecretarySet(Id, SanitizeString(secretary)));
		}

		private static string SanitizeMobilePhone(string number)
		{
			if (string.IsNullOrWhiteSpace(number)) return null;

			var result = SanitizePhoneNumber(number);

			if(result.StartsWith("39") & result.Length > 10)
			{
				result = "+" + result;
			}

			if (!result.StartsWith("+39"))
			{
				result = "+39" + result;
			}

			return result;
		}

		private static string SanitizeLandlineNumber(string number)
		{
			if (string.IsNullOrWhiteSpace(number)) return null;

			return SanitizePhoneNumber(number);
		}
		 
		private static string SanitizePhoneNumber(string number)
		{
			var result = Regex.Replace(number, "[^+0-9]", "");

			return result;
		}

		private static string SanitizeString(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return null;

			return value.ToLowerInvariant();
		}

		#region Apply events

		private void Apply(ReferentCreated @event)
		{
			Id = @event.AggregateId;
		}

		private void Apply(ReferentNameChanged @event)
		{
			FirstName = @event.FirstName;
			LastName = @event.LastName;
		}

		private void Apply(ReferentClientSet @event)
		{
			ClientId = @event.ClientId;
		}

		private void Apply(ReferentAreaSet @event)
		{
			Area = @event.Area;
		}

		private void Apply(ReferentSecretarySet @event)
		{
			Secretary = @event.Secretary;
		}

		private void Apply(ReferentEmailContactSet @event)
		{
			EmailAddress = @event.Address;
		}

		private void Apply(ReferentMobileContactSet @event)
		{
			MobileNumber = @event.Number;
		}

		private void Apply(ReferentLandlineContactSet @event)
		{
			LandlineNumber = @event.Number;
		}

		#endregion

	}
}
