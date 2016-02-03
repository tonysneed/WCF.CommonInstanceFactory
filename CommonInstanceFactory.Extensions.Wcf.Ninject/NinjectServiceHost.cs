using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Ninject;
using CommonInstanceFactory.Adapters.Ninject;

namespace CommonInstanceFactory.Extensions.Wcf.Ninject
{
	public class NinjectServiceHost : ServiceHost
	{
		public NinjectServiceHost(IKernel container, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
			// Create instance factory
			object instanceFactory = ReflectionHelper.CreateGenericInstance
				(typeof (NinjectInstanceFactory<>), serviceType, container);

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
