using Atlas.Fluent.Impl;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IObservableServiceResolver"/>.
	/// </summary>
	public static class ObservableServiceResolverExtensions
	{
		/// <summary>
		/// 	Creates a pending observer for a service.
		/// </summary>
		/// <typeparam name="TService">The service the observer should be notified of.</typeparam>
		public static IPendingObserver<TService> Observe<TService>(this IObservableServiceResolver @this) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));

			return new PendingObserver<TService>(x => @this.Observe<TService>(x));
		}
	}
}
