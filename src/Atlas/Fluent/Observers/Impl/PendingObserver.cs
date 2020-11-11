using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IPendingObserver{TService}"/>.
	/// </summary>
	/// <typeparam name="TService">The type of the service to observe.</typeparam>
	public class PendingObserver<TService> : IPendingObserver<TService> where TService : notnull
	{
		/// <inheritdoc cref="IPendingObserver{TService}.Applicator"/>
		public Action<IServiceObserver<TService>> Applicator { get; }

		/// <summary>
		/// 	Constructs an instance of <see cref="PendingObserver{TService}"/>.
		/// </summary>
		/// <param name="applicator">The function to set the observer.</param>
		public PendingObserver(Action<IServiceObserver<TService>> applicator)
		{
			Guard.Null(applicator, nameof(applicator));

			Applicator = applicator;
		}
	}
}
