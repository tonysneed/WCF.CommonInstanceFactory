using Microsoft.Practices.Unity;

namespace CommonInstanceFactory.Sample.DependencyResolution.Unity
{
    public static class ContainerExtensions
	{
		public static void Load<TModuleType>(this IUnityContainer container)
			where TModuleType : class, IUnityModule, new()
		{
			var x = new TModuleType();
			x.Load(container);
		}
	}
}
