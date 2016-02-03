using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ninject;
using CommonInstanceFactory.Adapters.Ninject;
using CommonInstanceFactory.Sample.DependencyResolution.Ninject.Modules;
using CommonInstanceFactory.Sample.Services;

namespace CommonInstanceFactory.Tests.Ninject
{
	[TestFixture]
	public class NinjectAdapterTests
	{
		private IKernel _container;

		[TestFixtureSetUp]
		public void TestSetup()
		{
			_container = new StandardKernel();
			_container.Load<GreetingModule>();
		}

		[Test]
		public void Ninject_Adapter_Should_Get_GreetingService()
		{
			// Arrange
			var resolver = new NinjectInstanceFactory<GreetingService>(_container);

			// Act
			var greeter = resolver.GetInstance();
			string greeting = greeter.Greet("Tony");

			// Assert
			Assert.That(greeting, Is.StringMatching("Howdy Tony"));
		}
	}
}
