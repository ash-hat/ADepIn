using System;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	Represent an observer not yet registered to a <see cref="IObservableServiceResolver" />.
	/// </summary>
	/// <typeparam name="TService">The type of the service to observe.</typeparam>
	public interface IPendingObserver<TService> where TService : notnull
	{
	 	/// <summary>
		/// 	The function to set the observer.
		/// </summary>
	 	Action<IServiceObserver<TService>> Applicator { get; }
	}
}