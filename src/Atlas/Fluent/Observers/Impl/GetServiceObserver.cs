using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service observer which calls a function, that accepts only a service, upon notification.
	/// </summary>
	public class GetServiceObserver<TService> : IServiceObserver<TService> where TService : notnull
	{
		private readonly Action<TService> _callback;

		/// <summary>
		/// 	Constructs an instance of <see cref="ServicesServiceObserver{TService}"/>.
		/// </summary>
		/// <param name="callback">The function to invoke upon notification.</param>
		public GetServiceObserver(Action<TService> callback)
		{
			Guard.Null(callback, nameof(callback));

			_callback = callback;
		}

		/// <inheritdoc cref="IServiceObserver{TService}.Notify"/>
		public void Notify(IObservableServiceResolver? services, Func<TService> service)
		{
			Guard.Null(service, nameof(service));

			_callback(service());
		}
	}
}
