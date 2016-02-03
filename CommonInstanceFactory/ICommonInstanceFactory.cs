using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonInstanceFactory
{
	/// <summary>
	/// The generic Instance Factory interface. This interface is used
	/// to retrieve instances identified by type from a container.
	/// </summary>
	/// <typeparam name="TInstance">Type for requested instances.</typeparam>
	public interface ICommonInstanceFactory<TInstance>
	{
		/// <summary>
		/// Get an instance of a type.
		/// </summary>
		/// <returns>The requested instance.</returns>
		TInstance GetInstance();

		/// <summary>
		/// Get all instances registered with the container.
		/// </summary>
		/// <returns>The requested instances.</returns>
		IEnumerable<TInstance> GetAllInstances();

		/// <summary>
		/// Release an instance whose lifetime is managed by the container.
		/// </summary>
		/// <param name="instance">Instance to be released.</param>
		void ReleaseInstance(TInstance instance);
	}
}
