using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace CommonInstanceFactory.Adapters.Unity
{
    public class UnityInstanceFactory<TInstance> : CommonInstanceFactoryBase<TInstance>
		where TInstance : class
	{
		private readonly IUnityContainer _container;

		public UnityInstanceFactory(IUnityContainer container)
		{
			this._container = container;
		}

		protected override TInstance InternalGetInstance()
		{
			var instance = this._container.Resolve<TInstance>();
			return instance;
		}

		protected override IEnumerable<TInstance> InternalGetAllInstances()
		{
			var instances = this._container.ResolveAll<TInstance>();
			return instances;
		}

		protected override void InternalReleaseInstance(TInstance instance)
		{
		    this._container.Teardown(instance);
		}
	}
}
