using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonInstanceFactory
{
	/// <summary>
	/// Provides default implementation for ICommonInstanceFactory.
	/// Adapters should inherit from this class and override the abstract methods.
	/// </summary>
	/// <typeparam name="TInstance">Type for requested instances.</typeparam>
	public abstract class CommonInstanceFactoryBase<TInstance> : ICommonInstanceFactory<TInstance>
		where TInstance : class
	{
		/// <summary>
		/// Get an instance of a type.
		/// </summary>
		/// <returns>The requested instance.</returns>
		public TInstance GetInstance()
		{
			return InternalGetInstance();
		}

		/// <summary>
		/// Get all instances registered with the container.
		/// </summary>
		/// <returns>The requested instances.</returns>
		public IEnumerable<TInstance> GetAllInstances()
		{
			return InternalGetAllInstances();
		}

		/// <summary>
		/// Release an instance whose lifetime is managed by the container.
		/// </summary>
		/// <param name="instance">Instance to be released.</param>
		public void ReleaseInstance(TInstance instance)
		{
			InternalReleaseInstance(instance);
		}

		/// <summary>
		/// Override for container to get an instance of a type.
		/// </summary>
		/// <returns>The requested instance.</returns>
		protected abstract TInstance InternalGetInstance();

		/// <summary>
		/// Override for container to get all instances of a type.
		/// </summary>
		/// <returns>The requested instances.</returns>
		protected abstract IEnumerable<TInstance> InternalGetAllInstances();

		/// <summary>
		/// Override for a container to release an instance whose lifetime is managed 
		/// by the container.
		/// </summary>
		/// <param name="instance">Instance to be released.</param>
		protected abstract void InternalReleaseInstance(TInstance instance);
	}
}
