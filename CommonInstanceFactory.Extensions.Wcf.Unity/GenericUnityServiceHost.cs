﻿using System;
using System.ServiceModel;
using CommonInstanceFactory.Adapters.Unity;
using Microsoft.Practices.Unity;

namespace CommonInstanceFactory.Extensions.Wcf.Unity
{
    public class UnityServiceHost<TServiceType> : ServiceHost
		where TServiceType : class
	{
        public UnityServiceHost(IUnityContainer container, params Uri[] baseAddresses)
            : base(typeof(TServiceType), baseAddresses)
        {
            // Create instance factory
            ICommonInstanceFactory<TServiceType> instanceFactory
				= new UnityInstanceFactory<TServiceType>(container);

			// Add service behavior for routing instance provider
			Description.Behaviors.Add(new InjectedServiceBehavior<TServiceType>(instanceFactory));
		}
	}
}
