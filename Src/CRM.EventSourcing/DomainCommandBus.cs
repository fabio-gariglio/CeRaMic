using System;
using Castle.Core;
using Castle.MicroKernel;

namespace CRM.EventSourcing
{
	public class DomainCommandBus : IDomainCommandBus, ISingleton
	{
		private readonly IKernel _kernel;

		public DomainCommandBus(IKernel kernel)
		{
			_kernel = kernel;
		}

		public void Send(IDomainCommand command)
		{
			var commandType = command.GetType();
			var commandHandler = ResolveCommandHandler(commandType);

			((dynamic)commandHandler).Handle((dynamic)command);
		}

		//private static void InvokeHandler(IDomainCommandHandler commandHandler, IDomainCommand command)
		//{
		//	commandHandler.GetType()
		//								.GetMethod("Handle", new[] {command.GetType()})
		//								.Invoke(commandHandler, new object[] {command});
		//}

		private IDomainCommandHandler ResolveCommandHandler(Type commandType)
		{
			var handlerType = typeof (IDomainCommandHandler<>).MakeGenericType(commandType);

			return (IDomainCommandHandler) _kernel.Resolve(handlerType);
		}
	}
}