using System;

namespace Atlas
{
	/// <summary>
	/// 	Represents an object that is awaiting the availability of a service.
	/// </summary>
	/// <typeparam name="TService">The type of the service to observe.</typeparam>
	public interface IServiceObserver<TService> where TService : notnull
	{
		/// <summary>
		/// 	Notifies the observer that the service has become available.
		/// </summary>
		/// <param name="services">The services available to this observer.</param>
		/// <param name="handle">The getter of the service. The validity of this handle is not guaranteed if the kernel is mutated in any way.</param>
		void Notify(IObservableServiceResolver services, Func<TService> handle);
	}
}
