using CommonInstanceFactory.Adapters.Unity;
using CommonInstanceFactory.Sample.DependencyResolution.Unity;
using CommonInstanceFactory.Sample.DependencyResolution.Unity.Modules;
using CommonInstanceFactory.Sample.Services;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CommonInstanceFactory.Tests.Unity
{
    public class UnityAdapterTests
	{
		private IUnityContainer _container;

		[TestFixtureSetUp]
		public void TestSetup()
		{
			_container = new UnityContainer();
			_container.Load<GreetingModule>();
		}

		[Test]
		public void Unity_Adapter_Should_Get_GreetingService()
		{
			// Arrange
			var resolver = new UnityInstanceFactory<GreetingService>(_container);

			// Act
			var greeter = resolver.GetInstance();
			string greeting = greeter.Greet("Tony");

			// Assert
			Assert.That(greeting, Is.StringMatching("Howdy Tony"));
		}
	}
}
