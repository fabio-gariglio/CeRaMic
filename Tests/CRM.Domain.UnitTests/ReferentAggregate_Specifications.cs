using System.Linq;
using CRM.Domain.Referents;
using CRM.Referents.Events;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;

namespace CRM.Domain.UnitTests
{
	[Subject(typeof (ReferentAggregate))]
	public class ReferentAggregate_Specifications : WithSubject<ReferentAggregate>
	{
		public class When_Set_a_mobile_phone_contact
		{
			private Establish context = () => Registrar.New(r => r.For<string>().Use("test"));

			private Because of = () => Subject.SetMobileContact("+39 333 1234567");

			private It should_sanitize_the_number = () =>
			                                        {
				                                        var @event = Subject
					                                        .UncommittedChanges
					                                        .OfType<ReferentMobileContactSet>()
					                                        .First();

				                                        @event.Number.Should().Be("+393331234567");
			                                        };
		}
	}
}
