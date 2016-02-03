using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace CommonInstanceFactory.Extensions.Wcf
{
	/// <summary>
	/// Base class for a container-specific service host factory.
	/// Override abstract methods to create container-specific service host.
	/// </summary>
	/// <typeparam name="TContainer">Dependency injection container type.</typeparam>
	public abstract class InjectedServiceHostFactory<TContainer> : ServiceHostFactory
		where TContainer : class
	{
		/// <summary>
		/// Override to initialize selected container.
		/// </summary>
		/// <returns>Initialized container.</returns>
		protected abstract TContainer CreateContainer();

		/// <summary>
		/// Override to create container-specific service host.
		/// </summary>
		/// <param name="container">Selected dependency injection container.</param>
		/// <param name="serviceType">Specifies the type of service to host.</param>
		/// <param name="baseAddresses">The Uri array that contains the base addresses for the service hosted.</param>
		/// <returns>A container-specific service host for the type of service specified with a specific base address.</returns>
		protected abstract ServiceHost CreateInjectedServiceHost
			(TContainer container, Type serviceType, Uri[] baseAddresses);

		/// <summary>
		/// Creates a service host for a specified type of service with a specific base address.
		/// </summary>
		/// <param name="serviceType">Specifies the type of service to host.</param>
		/// <param name="baseAddresses">The Uri array that contains the base addresses for the service hosted.</param>
		/// <returns>A service host for the type of service specified with a specific base address.</returns>
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			TContainer container = CreateContainer();
			ServiceHost serviceHost = CreateInjectedServiceHost
				(container, serviceType, baseAddresses);
			return serviceHost;
		}
	}
}
