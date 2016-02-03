using CommonInstanceFactory.Sample.Data;
using CommonInstanceFactory.Sample.Interfaces;
using CommonInstanceFactory.Sample.Services;
using Microsoft.Practices.Unity;

namespace CommonInstanceFactory.Sample.DependencyResolution.Unity.Modules
{
    public class GreetingModule : IUnityModule
	{
		public void Load(IUnityContainer container)
		{
			container.RegisterType<IGreetingRepository, WesternGreetingRepository>();
			container.RegisterType<GreetingService>();
		}
	}
}
