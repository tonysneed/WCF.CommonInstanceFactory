using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace CommonInstanceFactory.Extensions.Wcf
{
	/// <summary>
	/// A container-agnostic implementation of IInstanceProvider for WCF services.
	/// InjectedServiceBehavior will use this class to create and release instances
	/// of the service type.
	/// </summary>
	/// <typeparam name="TServiceType">Type implementing a service contract.</typeparam>
	public class InjectedInstanceProvider<TServiceType> : IInstanceProvider
		where TServiceType : class
	{
		private readonly ICommonInstanceFactory<TServiceType> _container;

		/// <summary>
		/// Constructor accepting a common instance factory.
		/// </summary>
		/// <param name="container">Instance factory for selected container.</param>
		public InjectedInstanceProvider(ICommonInstanceFactory<TServiceType> container)
		{
			_container = container;
		}

		/// <summary>
		/// Get an instance using the specified container.
		/// </summary>
		/// <param name="instanceContext">The current instance context.</param>
		/// <param name="message">The message that triggered the creation of a service object.</param>
		/// <returns>Requested instance.</returns>
		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return GetInstance(instanceContext);
		}

		/// <summary>
		/// Get an instance using the specified container.
		/// </summary>
		/// <param name="instanceContext">The current instance context.</param>
		/// <returns>Requested instance.</returns>
		public object GetInstance(InstanceContext instanceContext)
		{
			TServiceType service = _container.GetInstance();
			return service;
		}

		/// <summary>
		/// Called when an instance context recycles a service object.
		/// </summary>
		/// <param name="instanceContext">The current instance context.</param>
		/// <param name="instance">The service instance to be recycled.</param>
		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			var service = instance as TServiceType;
			if (service != null)
			{
				try
				{
					_container.ReleaseInstance(service);
				}
				finally
				{
					var disposable = instance as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
	}
}
