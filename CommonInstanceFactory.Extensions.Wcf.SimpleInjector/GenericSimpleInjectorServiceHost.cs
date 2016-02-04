using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SimpleInjector;
using CommonInstanceFactory.Adapters.SimpleInjector;

namespace CommonInstanceFactory.Extensions.Wcf.SimpleInjector
{
	public class SimpleInjectorServiceHost<TServiceType> : ServiceHost
		where TServiceType : class
	{
	    public SimpleInjectorServiceHost(Container container, params Uri[] baseAddresses)
	        : this(container, typeof(TServiceType), baseAddresses)
	    {
	    }

	    public SimpleInjectorServiceHost(Container container, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
			// Create instance factory
			ICommonInstanceFactory<TServiceType> instanceFactory
				= new SimpleInjectorInstanceFactory<TServiceType>(container);

			// Add service behavior for routing instance provider
			Description.Behaviors.Add(new InjectedServiceBehavior<TServiceType>(instanceFactory));
		}
	}
}
