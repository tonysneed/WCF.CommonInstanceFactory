using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonInstanceFactory
{
	/// <summary>
	/// This class provides the ambient container for this application. If your
	/// framework defines such an ambient container, use InstanceFactory.Current
	/// to get it.
	/// </summary>
	/// <typeparam name="TInstance">Type for requested instances.</typeparam>
	public static class InstanceFactory<TInstance>
		where TInstance : class
	{
		private static ICommonInstanceFactory<TInstance> _instanceFactory;

		/// <summary>
		/// Set the current Instance Factory.
		/// </summary>
		/// <param name="instanceFactory">Instance factory selected to be the ambient factory.</param>
		public static void SetInstanceFactory(ICommonInstanceFactory<TInstance> instanceFactory)
		{
			_instanceFactory = instanceFactory;
		}

		/// <summary>
		/// The ambient Instance Factory.
		/// </summary>
		public static ICommonInstanceFactory<TInstance> Current
		{
			get { return _instanceFactory; }
		}
	}
}
