using System;
using Atlas.Fluent.Impl;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IPendingBinding{TService}" />.
	/// </summary>
	public static class PendingBindingExtensions
	{
		/// <summary>
		/// 	Binds a constant value to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="constant">A constant instance of the service.</param>
		/// <typeparam name="TService">The type of the service to bind to.</typeparam>
		public static void ToConstant<TService>(this IPendingBinding<TService> @this, TService constant) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(constant, nameof(constant));

			@this.Applicator(new ConstantServiceBinding<TService>(constant));
		}

		/// <summary>
		/// 	Binds a parameterless getter to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">A parameterless method to get an instance of the service.</param>
		/// <typeparam name="TService">The type of the service to bind to.</typeparam>
		public static IPendingScopedBinding<TService> ToMethod<TService>(this IPendingBinding<TService> @this, Func<TService> method) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return new PendingScopedBinding<TService>(@this.Applicator, new FunctionalServiceBinding<TService>(method));
		}

		/// <summary>
		/// 	Binds a contextual getter to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">A contextualized method to get an instance of the service.</param>
		/// <typeparam name="TService">The type of the service to bind to.</typeparam>
		public static IPendingScopedBinding<TService> ToMethod<TService>(this IPendingBinding<TService> @this, ServiceGetter<TService> method) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return new PendingScopedBinding<TService>(@this.Applicator, new FunctionalResolverServiceBinding<TService>(method));
		}
	}
}
