using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using CommonInstanceFactory.Adapters.Unity;
using Microsoft.Practices.Unity;

namespace CommonInstanceFactory.Extensions.Wcf.Unity
{
    public class UnityServiceHost : ServiceHost
	{
		public UnityServiceHost(IUnityContainer container, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
			// Create instance factory
			object instanceFactory = ReflectionHelper.CreateGenericInstance
				(typeof(UnityInstanceFactory<>), serviceType, container);

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
