using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector
{
	public static class ContainerExtensions
	{
		public static void Load<TModuleType>(this Container container)
			where TModuleType : class, ISimpleInjectorModule, new()
		{
			var x = new TModuleType();
			x.Load(container);
		}
	}
}
