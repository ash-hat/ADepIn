using System;
using Atlas.Fluent.Impl;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	Notifies a functional observer about the availability of a service, given the services and other services.
	/// </summary>
	/// <param name="services">The services available to the observer.</param>
	/// <param name="service">A premade <see cref="IServiceResolver.Get{TService}"/> call for the service that the observer has been awaiting availability of.</param>
	/// <typeparam name="TService">The type of service that the observer has been awaiting availability of.</typeparam>
	public delegate void ServiceNotifier<in TService>(IObservableServiceResolver services, TService service);

	/// <summary>
	/// 	A collection of extension methods for <see cref="IPendingObserver{TService}"/>.
	/// </summary>
	public static class PendingObserverExtensions
	{
		/// <summary>
		/// 	Observes a service using a callback that accepts both an <see cref="IObservableServiceResolver"/> and instance of <typeparamref name="TService"/>.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="callback">The function to call upon notification.</param>
		/// <typeparam name="TService">The type of service that the observer has been awaiting availability of.</typeparam>
		public static void ToMixedMethod<TService>(this IPendingObserver<TService> @this, ServiceNotifier<TService> callback) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(callback, nameof(callback));

			@this.Applicator(new MixedServiceObserver<TService>(callback));
		}

		/// <summary>
		/// 	Observes a service using a callback that accepts only an <see cref="IObservableServiceResolver"/>.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="callback">The function to call upon notification.</param>
		/// <typeparam name="TService">The type of service that the observer has been awaiting availability of.</typeparam>
		public static void ToServicesMethod<TService>(this IPendingObserver<TService> @this, Action<IObservableServiceResolver> callback) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(callback, nameof(callback));

			@this.Applicator(new ServicesServiceObserver<TService>(callback));
		}

		/// <summary>
		/// 	Observes a service using a callback that accepts only an instance of <typeparamref name="TService"/>.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="callback">The function to call upon notification.</param>
		/// <typeparam name="TService">The type of service that the observer has been awaiting availability of.</typeparam>
		public static void ToGetMethod<TService>(this IPendingObserver<TService> @this, Action<TService> callback) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(callback, nameof(callback));

			@this.Applicator(new GetServiceObserver<TService>(callback));
		}
	}
}
