using Atlas.Fluent.Impl;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IServiceBinder" />.
	/// </summary>
	public static class ServiceBinderExtensions
	{
		/// <summary>
		/// 	Creates a pending binding for a service.
		/// </summary>
		/// <typeparam name="TService">The type of the service to bind to.</typeparam>
		public static IPendingBinding<TService> Bind<TService>(this IServiceBinder @this) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));

			return new PendingBinding<TService>(@this.Bind);
		}
	}
}
