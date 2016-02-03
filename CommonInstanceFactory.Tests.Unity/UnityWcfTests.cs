using System;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Unity;
using CommonInstanceFactory.Sample.DependencyResolution.Unity;
using CommonInstanceFactory.Sample.Services;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CommonInstanceFactory.Tests.Unity
{
    public class UnityWcfTests
	{
		private IUnityContainer _container;
		private const string ServiceAddress = "http://localhost:1234/GreetingService";

		[TestFixtureSetUp]
		public void TestSetup()
		{
			_container = new UnityContainer();
			_container.Load<CommonInstanceFactory.Sample.DependencyResolution.Unity.Modules.GreetingModule>();
		}

		[Test]
		public void UnityServiceHost_Should_Use_Soap_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new UnityServiceHost<GreetingService>(_container);

			// Act
			using (var serviceHelper = new InjectedServiceHelper<IGreetingService>
				(serviceHost, ServiceAddress, new BasicHttpBinding()))
			{
				IGreetingService client = serviceHelper.Client;
				using ((IDisposable)client)
				{
					string greeting = client.Greet("Tony");

					// Assert
					Assert.That(greeting, Is.StringMatching("Howdy Tony"));
				}
			}
		}

		[Test]
		public void NonGeneric_UnityServiceHost_Should_Use_Soap_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new UnityServiceHost
				(_container, typeof(GreetingService));
			using (var serviceHelper = new InjectedServiceHelper<IGreetingService>
				(serviceHost, ServiceAddress, new BasicHttpBinding()))
			{
				IGreetingService client = serviceHelper.Client;
				using ((IDisposable)client)
				{
					// Act
					string greeting = client.Greet("Tony");

					// Assert
					Assert.That(greeting, Is.StringMatching("Howdy Tony"));
				}
			}
		}

		[Test]
		public void UnityServiceHost_Should_Use_Rest_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new UnityServiceHost<GreetingService>(_container);
			using (var serviceHelper = new InjectedServiceHelper<IGreetingService>
				(serviceHost, ServiceAddress, new WebHttpBinding()))
			{
				IGreetingService client = serviceHelper.Client;
				using ((IDisposable)client)
				{
					// Act
					string greeting = client.Greet("Tony");

					// Assert
					Assert.That(greeting, Is.StringMatching("Howdy Tony"));
				}
			}
		}

		[Test]
		public void NonGeneric_UnityServiceHost_Should_Use_Rest_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new UnityServiceHost
				(_container, typeof(GreetingService));
			using (var serviceHelper = new InjectedServiceHelper<IGreetingService>
				(serviceHost, ServiceAddress, new WebHttpBinding()))
			{
				IGreetingService client = serviceHelper.Client;
				using ((IDisposable)client)
				{
					// Act
					string greeting = client.Greet("Tony");

					// Assert
					Assert.That(greeting, Is.StringMatching("Howdy Tony"));
				}
			}
		}
	}
}
