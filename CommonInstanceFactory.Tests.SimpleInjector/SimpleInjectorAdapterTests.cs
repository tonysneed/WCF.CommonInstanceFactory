using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimpleInjector;
using CommonInstanceFactory.Adapters.SimpleInjector;
using CommonInstanceFactory.Sample.Services;
using CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector;
using CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector.Modules;

namespace CommonInstanceFactory.Tests.SimpleInjector
{
	public class SimpleInjectorAdapterTests
	{
		private Container _container;

		[TestFixtureSetUp]
		public void TestSetup()
		{
			_container = new Container();
			_container.Load<GreetingModule>();
		}

		[Test]
		public void SimpleInjector_Adapter_Should_Get_GreetingService()
		{
			// Arrange
			var resolver = new SimpleInjectorInstanceFactory<GreetingService>(_container);

			// Act
			var greeter = resolver.GetInstance();
			string greeting = greeter.Greet("Tony");

			// Assert
			Assert.That(greeting, Is.StringMatching("Howdy Tony"));
		}
	}
}
