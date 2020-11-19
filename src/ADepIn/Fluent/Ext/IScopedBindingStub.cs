using ADepIn.Fluent.Impl;

namespace ADepIn.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IScopedBindingStub{TService, TContext}"/>.
	/// </summary>
	public static class ExtIScopedBindingStub
	{
		/// <summary>
		/// 	Binds this binding as a singleton, contextful service; only one instance will be per context and it will persist for each call to the binding.
		/// </summary>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static void InSingletonScope<TService, TContext>(this IScopedBindingStub<TService, TContext> @this)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));

			@this.Applicator(new SingletonServiceBinding<TService, TContext>(@this.Binding));
		}

		/// <summary>
		/// 	Binds this binding as a singleton, contextless service; only one instance will be retrieved irregardless of context and it will persist for each call to the binding.
		/// </summary>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static void InSingletonNopScope<TService, TContext>(this IScopedBindingStub<TService, TContext> @this)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));

			@this.Applicator(new SingletonNopServiceBinding<TService, TContext>(@this.Binding));
		}

		/// <summary>
		/// 	Binds this binding as a transient service; a new instance will be retrieved for each call to the binding.
		/// </summary>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static void InTransientScope<TService, TContext>(this IScopedBindingStub<TService, TContext> @this)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));

			@this.Applicator(@this.Binding);
		}
	}
}
