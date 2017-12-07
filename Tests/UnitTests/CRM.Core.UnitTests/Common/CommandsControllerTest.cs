using CRM.Common;
using FluentAssertions;
using Machine.Specifications;
using NSubstitute;

namespace CRM.Core.UnitTests.Common
{
	[Subject("Encription Service")]
	public class EncriptionServiceTest
	{
		private static EncriptionService _target;

		private Because of = () =>
		                     {
													 _target = new EncriptionService();
		                     };

		private It should_calculate_hash_for_a_value = () => _target.CalculateHash("test").Should().Be("098f6bcd4621d373cade4e832627b4f6");
	}
}