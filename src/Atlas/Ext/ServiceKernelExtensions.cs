using System;
using System.Collections.Generic;
using System.Linq;

namespace Atlas
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IServiceKernel"/>.
	/// </summary>
	public static class ServiceKernelExtensions
	{
		private static Option<Type> MakeEntryModuleType(Type @this)
		{
			try
			{
				return Option.Some(typeof(IEntryModule<>).MakeGenericType(@this));
			}
			catch (ArgumentException)
			{
				return Option.None<Type>();
			}
		}

		/// <summary>
		/// 	Loads an entry module type into the service kernel.
		/// </summary>
		/// <param name="this"></param>
		/// <typeparam name="TModule">The type of the module.</typeparam>
		public static TModule LoadEntryType<TModule>(this IServiceKernel @this) where TModule : IEntryModule<TModule>, new()
		{
			Guard.Null(@this, nameof(@this));

			return (TModule) @this.LoadEntryTypeInternal(typeof(TModule));
		}

		/// <summary>
		/// 	Loads an entry module type into the service kernel.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="type">The type of the module.</param>
		public static Option<IModule> LoadEntryType(this IServiceKernel @this, Type type)
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(type, nameof(type));

			return MakeEntryModuleType(type)
				.Map(x => x.IsAssignableFrom(x)
					? Option.Some(@this.LoadEntryTypeInternal(type))
					: Option.None<IModule>())
				.Flatten();
		}

		private static IModule LoadEntryTypeInternal(this IServiceKernel @this, Type type)
		{
			var module = (IModule) Activator.CreateInstance(type)!;

			module.Load(@this);

			return module;
		}

		/// <summary>
		/// 	Loads any entry module types from an enumerable into the service kernel.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="types">The possible module types to load. May contain zero modules.</param>
		public static IEnumerable<IModule> LoadEntryTypes(this IServiceKernel @this, IEnumerable<Type> types)
		{
			Guard.Null(@this, nameof(@this));
			// ReSharper disable once PossibleMultipleEnumeration
			Guard.Null(types, nameof(types));

			// ReSharper disable once PossibleMultipleEnumeration
			return Guard.NullElement(types, nameof(types)).WhereSelect(x => @this.LoadEntryType(x));
		}
	}
}
