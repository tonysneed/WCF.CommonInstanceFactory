using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using CommonInstanceFactory.Sample.Data;
using CommonInstanceFactory.Sample.Interfaces;
using CommonInstanceFactory.Sample.Services;

namespace CommonInstanceFactory.Sample.DependencyResolution.Ninject.Modules
{
	public class GreetingModule : NinjectModule
	{
		public override void Load()
		{
			Kernel.Bind<IGreetingRepository>().To<WesternGreetingRepository>();
			Kernel.Bind<GreetingService>().ToSelf();
		}
	}
}
