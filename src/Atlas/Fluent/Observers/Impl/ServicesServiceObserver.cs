using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service observer which calls a function, that accepts only an observable service resolver, upon notification.
	/// </summary>
	public class ServicesServiceObserver<TService> : IServiceObserver<TService> where TService : notnull
	{
		private readonly Action<IObservableServiceResolver> _callback;

		/// <summary>
		/// 	Constructs an instance of <see cref="ServicesServiceObserver{TService}"/>.
		/// </summary>
		/// <param name="callback">The function to invoke upon notification.</param>
		public ServicesServiceObserver(Action<IObservableServiceResolver> callback)
		{
			Guard.Null(callback, nameof(callback));

			_callback = callback;
		}

		/// <inheritdoc cref="IServiceObserver{TService}.Notify"/>
		public void Notify(IObservableServiceResolver services, Func<TService>? service)
		{
			Guard.Null(services, nameof(services));

			_callback(services);
		}
	}
}
