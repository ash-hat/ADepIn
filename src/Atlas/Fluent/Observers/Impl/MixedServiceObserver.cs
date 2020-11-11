using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service observer which calls a function, that accepts both a service resolver and service, upon notification.
	/// </summary>
	public class MixedServiceObserver<TService> : IServiceObserver<TService> where TService : notnull
	{
		private readonly ServiceNotifier<TService> _callback;

		/// <summary>
		/// 	Constructs an instance of <see cref="ServicesServiceObserver{TService}"/>.
		/// </summary>
		/// <param name="callback">The function to invoke upon notification.</param>
		public MixedServiceObserver(ServiceNotifier<TService> callback)
		{
			Guard.Null(callback, nameof(callback));

			_callback = callback;
		}

		/// <inheritdoc cref="IServiceObserver{TService}.Notify"/>
		public void Notify(IObservableServiceResolver services, Func<TService> service)
		{
			Guard.Null(services, nameof(services));
			Guard.Null(service, nameof(service));

			_callback(services, service());
		}
	}
}
