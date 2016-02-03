using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;
using CommonInstanceFactory.Sample.Data;
using CommonInstanceFactory.Sample.Interfaces;
using CommonInstanceFactory.Sample.Services;

namespace CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector.Modules
{
	public class GreetingModule : ISimpleInjectorModule
	{
		public void Load(Container container)
		{
			container.Register<IGreetingRepository, WesternGreetingRepository>();
			container.Register<GreetingService>();
		}
	}
}
