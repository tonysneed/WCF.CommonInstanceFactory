using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using NUnit.Framework;
using SimpleInjector;
using CommonInstanceFactory.Sample.Services;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.SimpleInjector;
using CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector;
using CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector.Modules;

namespace CommonInstanceFactory.Tests.SimpleInjector
{
	public class SimpleInjectorWcfTests
	{
		private Container _container;
		private const string ServiceAddress = "http://localhost:1234/GreetingService";

		[TestFixtureSetUp]
		public void TestSetup()
		{
			_container = new Container();
			_container.Load<GreetingModule>();
		}

		[Test]
		public void SimpleInjectorServiceHost_Should_Use_Soap_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new SimpleInjectorServiceHost<GreetingService>(_container);

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
		public void NonGeneric_SimpleInjectorServiceHost_Should_Use_Soap_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new SimpleInjectorServiceHost
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
		public void SimpleInjectorServiceHost_Should_Use_Rest_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new SimpleInjectorServiceHost<GreetingService>(_container);
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
		public void NonGeneric_SimpleInjectorServiceHost_Should_Use_Rest_GreetingService()
		{
			// Arrange
			ServiceHost serviceHost = new SimpleInjectorServiceHost
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
