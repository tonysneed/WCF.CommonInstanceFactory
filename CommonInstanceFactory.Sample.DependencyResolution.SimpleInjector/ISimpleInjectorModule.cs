using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleInjector;

namespace CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector
{
	public interface ISimpleInjectorModule
	{
		void Load(Container container);
	}
}
