using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonInstanceFactory.Extensions.Wcf
{
	/// <summary>
	/// Helper class to create a generic type using reflection.
	/// This class can be used by container-specific Instance Factory WCF extensions
	/// to create a non-generic service host for use in config files.
	/// </summary>
	public static class ReflectionHelper
	{
		/// <summary>
		/// Create a closed generic instance using reflection.
		/// </summary>
		/// <param name="genericType">Parent generic type.</param>
		/// <param name="argType">Generic type argument.</param>
		/// <param name="args">Other arguments.</param>
		/// <returns>Closed generic type instance.</returns>
		public static object CreateGenericInstance(Type genericType, Type argType, params object[] args)
		{
			string typeName = string.Format("{0}[[{2}]], {1}",
				genericType.FullName, genericType.Assembly.FullName, argType.AssemblyQualifiedName);
			Type type = Type.GetType(typeName);
			if (type == null) return null;
			object instance = Activator.CreateInstance(type, args);
			return instance;
		}
	}
}
