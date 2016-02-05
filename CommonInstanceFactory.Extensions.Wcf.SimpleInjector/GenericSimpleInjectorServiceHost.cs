using System;
using System.ServiceModel;
using CommonInstanceFactory.Adapters.SimpleInjector;
using SimpleInjector;

namespace CommonInstanceFactory.Extensions.Wcf.SimpleInjector
{
    public class SimpleInjectorServiceHost<TServiceType> : ServiceHost
        where TServiceType : class
    {
        public SimpleInjectorServiceHost(Container container, params Uri[] baseAddresses)
            : base(typeof(TServiceType), baseAddresses)
        {
            // Create instance factory
            ICommonInstanceFactory<TServiceType> instanceFactory
                = new SimpleInjectorInstanceFactory<TServiceType>(container);

            // Add service behavior for routing instance provider
            Description.Behaviors.Add(new InjectedServiceBehavior<TServiceType>(instanceFactory));
        }
    }
}
