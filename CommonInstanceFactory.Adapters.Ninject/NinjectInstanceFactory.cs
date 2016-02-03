using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace CommonInstanceFactory.Adapters.Ninject
{
	public class NinjectInstanceFactory<TInstance> : CommonInstanceFactoryBase<TInstance>
		where TInstance : class
	{
		private readonly IKernel _container;

		public NinjectInstanceFactory(IKernel container)
		{
			_container = container;
		}

		protected override TInstance InternalGetInstance()
		{
			var instance = _container.Get<TInstance>();
			return instance;
		}

		protected override IEnumerable<TInstance> InternalGetAllInstances()
		{
			var instances = _container.GetAll<TInstance>();
			return instances;
		}

		protected override void InternalReleaseInstance(TInstance instance)
		{
			_container.Release(instance);
		}
	}
}
