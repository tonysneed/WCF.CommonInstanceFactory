using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using SimpleInjector;
using CommonInstanceFactory.Adapters.SimpleInjector;

namespace CommonInstanceFactory.Extensions.Wcf.SimpleInjector
{
	public class SimpleInjectorServiceHost : ServiceHost
	{
		public SimpleInjectorServiceHost(Container container, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
			// Create instance factory
			object instanceFactory = ReflectionHelper.CreateGenericInstance
				(typeof(SimpleInjectorInstanceFactory<>), serviceType, container);

			// Add service behavior for routing instance provider
			if (instanceFactory != null)
			{
				var behavior = ReflectionHelper.CreateGenericInstance(typeof(InjectedServiceBehavior<>),
					serviceType, instanceFactory) as IServiceBehavior;
				if (behavior != null)
				{
					Description.Behaviors.Add(behavior);
				}
			}
		}
	}
}
