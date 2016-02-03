using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace CommonInstanceFactory.Extensions.Wcf
{
	/// <summary>
	/// Helper class for spinning up WCF services for use with integration tests.
	/// </summary>
	/// <typeparam name="TServiceContract">Service contract type.</typeparam>
	public class InjectedServiceHelper<TServiceContract> : IDisposable
	{
		private bool _disposed;
		private readonly ServiceHost _serviceHost;
		private readonly TServiceContract _client;
		private readonly ServiceEndpoint _clientEndpoint;

		/// <summary>
		/// Constructor for creating InjectedServiceHelper.
		/// </summary>
		/// <param name="serviceHost">Container-specific service host.</param>
		/// <param name="address">Service endpoint address.</param>
		/// <param name="binding">Service endpoint binding.</param>
		public InjectedServiceHelper(ServiceHost serviceHost, string address, Binding binding)
		{
			// Add endpoint
			_serviceHost = serviceHost;
			_serviceHost.AddServiceEndpoint(typeof(TServiceContract), binding, address);

			// Client channel factory
			var clientFactory = new ChannelFactory<TServiceContract>(binding, address);

			// Add web behavior
			if (binding.GetType() == typeof(WebHttpBinding))
			{
				var webBehavior = new WebHttpBehavior
				{
					AutomaticFormatSelectionEnabled = true,
					HelpEnabled = true,
					FaultExceptionEnabled = true
				};
				_serviceHost.Description.Endpoints[0].Behaviors.Add(webBehavior);
				clientFactory.Endpoint.Behaviors.Add(webBehavior);
			}

			// Configure client endpoint
			_clientEndpoint = clientFactory.Endpoint;

			// Add service metadata
			var metadataBehavior = new ServiceMetadataBehavior();
			if (binding.GetType() == typeof(BasicHttpBinding))
			{
				metadataBehavior.HttpGetEnabled = true;
				metadataBehavior.HttpGetUrl = new Uri(address);
			}
			_serviceHost.Description.Behaviors.Add(metadataBehavior);

			// Open service host
			_serviceHost.Open();

			// Create client channel
			_client = clientFactory.CreateChannel();
		}

		/// <summary>
		/// WCF service host.
		/// </summary>
		public ServiceHost ServiceHost
		{
			get
			{
				if (_disposed)
					throw new ObjectDisposedException(GetType().Name);
				return _serviceHost;
			}
		}

		/// <summary>
		/// Client used to invoke the service.
		/// </summary>
		public TServiceContract Client
		{
			get
			{
				if (_disposed)
					throw new ObjectDisposedException(GetType().Name);
				return _client;
			}
		}

		/// <summary>
		/// Endpoint created by the InjectedServiceHelper.
		/// </summary>
		public ServiceEndpoint ClientEndpoint
		{
			get
			{
				if (_disposed)
					throw new ObjectDisposedException(GetType().Name);
				return _clientEndpoint;
			}
		}

		/// <summary>
		/// Disposes the service host and client.
		/// </summary>
		public void Dispose()
		{
			if (!_disposed)
			{
				((IDisposable)_serviceHost).Dispose();
				((IDisposable)_client).Dispose();
				_disposed = true;
			}
		}
	}
}
