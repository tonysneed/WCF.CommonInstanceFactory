using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using Ninject;
using NinjectModules = CommonInstanceFactory.Sample.DependencyResolution.Ninject.Modules;

namespace CommonInstanceFactory.Sample.Hosting.Web.ServiceHostFactories
{
	public class NinjectServiceHostFactory : InjectedServiceHostFactory<IKernel>
	{
		protected override IKernel CreateContainer()
		{
			IKernel container = new StandardKernel();
			container.Load<NinjectModules.GreetingModule>();
			return container;
		}

		protected override ServiceHost CreateInjectedServiceHost
			(IKernel container, Type serviceType, Uri[] baseAddresses)
		{
			ServiceHost serviceHost = new NinjectServiceHost
				(container, serviceType, baseAddresses);
			return serviceHost;
		}
	}
}