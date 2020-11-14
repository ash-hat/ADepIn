using Atlas.Fluent.Impl;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IBindingStub{TService, TContext}"/>.
	/// </summary>
	public static class ExtIBindingStub
	{
		/// <summary>
		/// 	Binds a constant value to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="constant">A constant instance of the service.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static void ToConstant<TService, TContext>(this IBindingStub<TService, TContext> @this, TService constant)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(constant, nameof(constant));

			@this.Applicator(new ConstantServiceBinding<TService, TContext>(constant));
		}

		private static ScopedBindingStub<TService, TContext> ToScoped<TService, TContext>(this IBindingStub<TService, TContext> @this, IServiceBinding<TService, TContext> binding)
			where TService : notnull
			where TContext : notnull
		{
			return new ScopedBindingStub<TService, TContext>(@this.Applicator, binding);
		}

		/// <summary>
		/// 	Binds a full callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The full binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IScopedBindingStub<TService, TContext> ToWholeMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, WholeBindingImpl<TService, TContext> method)
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
		public static IScopedBindingStub<TService, TContext> ToRecursiveMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, RecursiveBindingImpl<TService> method)
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
		public static IScopedBindingStub<TService, TContext> ToContextualMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, ContextualBindingImpl<TService, TContext> method)
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
		public static IScopedBindingStub<TService, TContext> ToPureMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, PureBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		/// <summary>
		/// 	Binds a full, always Some, callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The full binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IScopedBindingStub<TService, TContext> ToWholeNopMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, WholeNopBindingImpl<TService, TContext> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		/// <summary>
		/// 	Binds a recursive, always Some, callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The recursive binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>		
		public static IScopedBindingStub<TService, TContext> ToRecursiveNopMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, RecursiveNopBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		/// <summary>
		/// 	Binds a contextual, always Some, callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The contextual binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IScopedBindingStub<TService, TContext> ToContextualNopMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, ContextualNopBindingImpl<TService, TContext> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}

		/// <summary>
		/// 	Binds a pure, always Some, callback method to the service.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="method">The pure binding implementation to use when this binding is called.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IScopedBindingStub<TService, TContext> ToPureNopMethod<TService, TContext>(this IBindingStub<TService, TContext> @this, PureNopBindingImpl<TService> method)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(method, nameof(method));

			return @this.ToScoped(new FunctionServiceBinding<TService, TContext>(method));
		}
	}
}
