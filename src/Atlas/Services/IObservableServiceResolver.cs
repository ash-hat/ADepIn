namespace Atlas
{
	/// <summary>
	/// 	Represents a service getter with a notifier of when a service is available.
	/// </summary>
	public interface IObservableServiceResolver : IServiceResolver
	{
		/// <summary>
		/// 	Binds an observer to the notification of a service's availability.
		/// 	If the service is already available, the observer will immediately be invoked.
		/// </summary>
		/// <param name="observer">The observer to bind.</param>
		/// <typeparam name="TService">The service the observer should be notified of.</typeparam>
		void Observe<TService>(IServiceObserver<TService> observer) where TService : notnull;
	}
}
