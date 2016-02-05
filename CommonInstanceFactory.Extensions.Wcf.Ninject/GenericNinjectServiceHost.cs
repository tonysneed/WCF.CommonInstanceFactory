using System;
using System.ServiceModel;
using CommonInstanceFactory.Adapters.Ninject;
using Ninject;

namespace CommonInstanceFactory.Extensions.Wcf.Ninject
{
    public class NinjectServiceHost<TServiceType> : ServiceHost
        where TServiceType : class
    {
        public NinjectServiceHost(IKernel container, params Uri[] baseAddresses)
            : base(typeof(TServiceType), baseAddresses)
        {
            // Create instance factory
            ICommonInstanceFactory<TServiceType> instanceFactory
                = new NinjectInstanceFactory<TServiceType>(container);

            // Add service behavior for routing instance provider
            Description.Behaviors.Add(new InjectedServiceBehavior<TServiceType>(instanceFactory));
        }
    }
}
