using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using NUnit.Framework;
using Ninject;
using CommonInstanceFactory.Sample.DependencyResolution.Ninject.Modules;
using CommonInstanceFactory.Sample.Services;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;

namespace CommonInstanceFactory.Tests.Ninject
{
	[TestFixture]
	public class NinjectWcfTests
	{
		private IKernel _container;
		private const string ServiceAddress = "http://localhost:1234/GreetingService";

		[TestFixtureSetUp]
		public void TestSetup()
		{
			_container = new StandardKernel();
			_container.Load<GreetingModule>();
		}

		[Test]
		public void NinjectServiceHost_Should_Use_Soap_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new NinjectServiceHost<GreetingService>
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
		public void NonGeneric_NinjectServiceHost_Should_Use_Soap_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new NinjectServiceHost
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
		public void NinjectServiceHost_Should_Use_Rest_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new NinjectServiceHost<GreetingService>
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

		[Test]
		public void NonGeneric_NinjectServiceHost_Should_Use_Rest_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new NinjectServiceHost
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
