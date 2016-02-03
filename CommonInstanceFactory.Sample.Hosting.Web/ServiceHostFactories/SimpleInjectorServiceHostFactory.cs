using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.SimpleInjector;
using CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector;
using SimpleInjector;
using SimpleInjectorModules = CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector.Modules;

namespace CommonInstanceFactory.Sample.Hosting.Web.ServiceHostFactories
{
	public class SimpleInjectorServiceHostFactory : InjectedServiceHostFactory<Container>
	{
		protected override Container CreateContainer()
		{
			var container = new Container();
			container.Load<SimpleInjectorModules.GreetingModule>();
			return container;
		}

		protected override ServiceHost CreateInjectedServiceHost
			(Container container, Type serviceType, Uri[] baseAddresses)
		{
			ServiceHost serviceHost = new SimpleInjectorServiceHost
				(container, serviceType, baseAddresses);
			return serviceHost;
		}
	}
}