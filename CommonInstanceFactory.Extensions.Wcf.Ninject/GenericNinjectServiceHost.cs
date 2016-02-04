using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Ninject;
using CommonInstanceFactory.Adapters.Ninject;

namespace CommonInstanceFactory.Extensions.Wcf.Ninject
{
	public class NinjectServiceHost<TServiceType> : ServiceHost
		where TServiceType : class
	{
	    public NinjectServiceHost(IKernel container, params Uri[] baseAddresses)
	        : this(container, typeof(TServiceType), baseAddresses)
	    {
	    }

	    public NinjectServiceHost(IKernel container, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
			// Create instance factory
			ICommonInstanceFactory<TServiceType> instanceFactory
				= new NinjectInstanceFactory<TServiceType>(container);

			// Add service behavior for routing instance provider
			Description.Behaviors.Add(new InjectedServiceBehavior<TServiceType>(instanceFactory));
		}
	}
}
