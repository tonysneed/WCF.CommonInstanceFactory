using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace CommonInstanceFactory.Extensions.Wcf
{
	/// <summary>
	/// Added by a container-specific service host to use the container to provide instances
	/// of the service type.
	/// </summary>
	/// <typeparam name="TServiceType">Type implementing a service contract.</typeparam>
	public class InjectedServiceBehavior<TServiceType> : IServiceBehavior
		where TServiceType : class
	{
		private readonly ICommonInstanceFactory<TServiceType> _container;

		/// <summary>
		/// Constructor accepting a common instance factory.
		/// </summary>
		/// <param name="container">Instance factory for selected container.</param>
		public InjectedServiceBehavior(ICommonInstanceFactory<TServiceType> container)
		{
			_container = container;
		}

		/// <summary>
		/// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
		/// </summary>
		/// <param name="serviceDescription">The service description.</param>
		/// <param name="serviceHostBase">The host that is currently being built.</param>
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
			{
				var cd = cdb as ChannelDispatcher;
				if (cd != null)
				{
					foreach (EndpointDispatcher ed in cd.Endpoints)
					{
						ed.DispatchRuntime.InstanceProvider
							= new InjectedInstanceProvider<TServiceType>(_container);
					}
				}
			}
		}

		/// <summary>
		/// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
		/// </summary>
		/// <param name="serviceDescription">The service description.</param>
		/// <param name="serviceHostBase">The service host that is currently being constructed.</param>
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		/// <summary>
		/// Provides the ability to pass custom data to binding elements to support the contract implementation.
		/// </summary>
		/// <param name="serviceDescription">The service description of the service.</param>
		/// <param name="serviceHostBase">The host of the service.</param>
		/// <param name="endpoints">The service endpoints.</param>
		/// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}
	}
}
