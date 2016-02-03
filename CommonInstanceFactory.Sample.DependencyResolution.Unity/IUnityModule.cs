using Microsoft.Practices.Unity;

namespace CommonInstanceFactory.Sample.DependencyResolution.Unity
{
    public interface IUnityModule
	{
		void Load(IUnityContainer container);
	}
}
