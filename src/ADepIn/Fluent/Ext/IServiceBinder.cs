using ADepIn.Fluent.Impl;

namespace ADepIn.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IServiceBinder" />.
	/// </summary>
	public static class ExtIServiceBinder
	{
		/// <summary>
		/// 	Creates a pending binding for a service.
		/// </summary>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		public static IBindingStub<TService, TContext> Bind<TService, TContext>(this IServiceBinder @this)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(@this, nameof(@this));

			return new BindingStub<TService, TContext>(@this.Bind);
		}

		/// <summary>
		/// 	Creates a pending binding for a service that expects no context (<see cref="Unit"/>).
		/// </summary>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		public static IBindingStub<TService, Unit> Bind<TService>(this IServiceBinder @this)
			where TService : notnull
		{
			Guard.Null(@this, nameof(@this));

			return new BindingStub<TService, Unit>(@this.Bind);
		}
	}
}
