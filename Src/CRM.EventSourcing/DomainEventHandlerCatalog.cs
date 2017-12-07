using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using CRM.Reflection;

namespace CRM.EventSourcing
{
	public class DomainEventHandlerCatalog : IDomainEventHandlerCatalog, ISingleton
	{
		private class HandlerDependency
		{
			public IDomainEventHandler Handler;

			public IEnumerable<Type> Dependencies;

			public bool WaitingForDependencies;

			public int Priority;

			public HandlerDependency()
			{
				Dependencies = Enumerable.Empty<Type>();
				WaitingForDependencies = false;
			}
		}

		private readonly IKernel _kernel;

		public DomainEventHandlerCatalog(IKernel kernel)
		{
			_kernel = kernel;
		}

		public IEnumerable<IDomainEventHandler> GetHandlers(Type eventType)
		{
			var handlers = GetHandlersByType(eventType).ToArray();

			var result = handlers.Select(h => CreateHandlerDependency(h, handlers)).ToList();

			var counter = handlers.Length;
			var index = 0;
			var satisfiedTypes = new List<Type>();

			if (counter == 0) return Enumerable.Empty<IDomainEventHandler>();

			do
			{
				var currentItem = result[index];

				if (currentItem.WaitingForDependencies)
				{
					currentItem.WaitingForDependencies = currentItem.Dependencies.Any(d => !satisfiedTypes.Contains(d));
				}

				if (currentItem.WaitingForDependencies)
				{
					result.Remove(currentItem);
					result.Add(currentItem);
					continue;
				}
				
				satisfiedTypes.Add(currentItem.Handler.GetType());
				index++;
			} while (index < counter);

			return result.OrderByDescending(h => h.Priority).Select(h => h.Handler);
		}

		private HandlerDependency CreateHandlerDependency(
			IDomainEventHandler handler,
			IEnumerable<IDomainEventHandler> handlers)
		{
			var handlerDependency = new HandlerDependency {Handler = handler};

			var handlerDepencenciesAttribute = handler
				.GetType()
				.GetCustomAttributes(typeof (HandlerDepencenciesAttribute), true)
				.FirstOrDefault();

			if (null != handlerDepencenciesAttribute)
			{
				var dependencies = ((HandlerDepencenciesAttribute) handlerDepencenciesAttribute).Dependencies;
				handlerDependency.Dependencies = dependencies.Where(d => handlers.Any(d.IsInstanceOfType));
			}

			var handlerPriorityAttribute = handler
				.GetType()
				.GetCustomAttributes(typeof (HandlerPriorityAttribute), true)
				.FirstOrDefault();

			if (null != handlerPriorityAttribute)
			{
				var priority = ((HandlerPriorityAttribute) handlerPriorityAttribute).Priority;
				handlerDependency.Priority = priority;
			}

			handlerDependency.WaitingForDependencies = handlerDependency.Dependencies.Any();

			return handlerDependency;
		}

		private IEnumerable<IDomainEventHandler> GetHandlersByType(Type eventType)
		{
			var handlers = new List<IDomainEventHandler>();

			foreach (var type in eventType.GetHierarchy().Where(IsDomainEvent))
			{
				var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(type);

				handlers.AddRange(_kernel.ResolveAll(handlerType).OfType<IDomainEventHandler>());
			}

			return handlers;
		}

		private static bool IsDomainEvent(Type type)
		{
			return typeof(IDomainEvent).IsAssignableFrom(type);
		}
	}
}