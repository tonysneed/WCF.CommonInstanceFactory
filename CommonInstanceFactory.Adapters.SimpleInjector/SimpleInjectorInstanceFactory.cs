using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace CommonInstanceFactory.Adapters.SimpleInjector
{
	public class SimpleInjectorInstanceFactory<TInstance> : CommonInstanceFactoryBase<TInstance>
		where TInstance : class
	{
		private readonly Container _container;

		public SimpleInjectorInstanceFactory(Container container)
		{
			_container = container;
		}

		protected override TInstance InternalGetInstance()
		{
			var instance = _container.GetInstance<TInstance>();
			return instance;
		}

		protected override IEnumerable<TInstance> InternalGetAllInstances()
		{
			var instances = _container.GetAllInstances<TInstance>();
			return instances;
		}

		protected override void InternalReleaseInstance(TInstance instance)
		{
			// Calling Release is not necessary because SimpleInjector does not manage instance lifetime.
			// Instead you should call Dispose directly on instances that implement IDisposable.
		}
	}
}
