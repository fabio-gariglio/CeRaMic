using System;
using System.Runtime.Serialization;

namespace CRM.EventSourcing.Exceptions
{
	[Serializable]
	public class InfrastructureException : Exception
	{
		public InfrastructureException()
		{ }

		public InfrastructureException(string message)
			: base(message) { }

		public InfrastructureException(string format, params object[] args)
			: base(string.Format(format, args)) { }

		public InfrastructureException(string message, Exception innerException)
			: base(message, innerException) { }

		public InfrastructureException(string format, Exception innerException, params object[] args)
			: base(string.Format(format, args), innerException) { }

		protected InfrastructureException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}
}
