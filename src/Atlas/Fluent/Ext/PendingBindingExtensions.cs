using System;
using Atlas.Fluent.Impl;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IPendingBinding{TService, TContext}"/>.
	/// </summary>
	public static class PendingBindingExtensions
	{
		/// <summary>
		/// 	Binds a constant value to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="constant">A constant instance of the service.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static void ToConstant<TService, TContext>(this IPendingBinding<TService, TContext> @this, TService constant)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(constant, nameof(constant));

			@this.Applicator(new ConstantServiceBinding<TService, TContext>(constant));
		}

		private static PendingScopedBinding<TService, TContext> ToScoped<TService, TContext>(this IPendingBinding<TService, TContext> @this, IServiceBinding<TService, TContext> binding)
			where TService : notnull
			where TContext : notnull
		{
			return new PendingScopedBinding<TService, TContext>(@this.Applicator, binding);
		}

		/// <summary>
		/// 	Binds a full callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The full binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IPendingScopedBinding<TService, TContext> ToWholeMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, WholeBindingImpl<TService, TContext> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		/// <summary>
		/// 	Binds a contextual callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The contextual binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IPendingScopedBinding<TService, TContext> ToRecursiveMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, RecursiveBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		/// <summary>
		/// 	Binds a recursive callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The recursive binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IPendingScopedBinding<TService, TContext> ToContextualMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, RecursiveBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		/// <summary>
		/// 	Binds a pure callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The pure binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IPendingScopedBinding<TService, TContext> ToPureMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, PureBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		public static IPendingScopedBinding<TService, TContext> ToWholeNopMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, WholeNopBindingImpl<TService, TContext> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		public static IPendingScopedBinding<TService, TContext> ToRecursiveNopMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, RecursiveNopBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		public static IPendingScopedBinding<TService, TContext> ToContextualNopMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, RecursiveNopBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		public static IPendingScopedBinding<TService, TContext> ToPureNopMethod<TService, TContext>(this IPendingBinding<TService, TContext> @this, PureNopBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}
	}
}
