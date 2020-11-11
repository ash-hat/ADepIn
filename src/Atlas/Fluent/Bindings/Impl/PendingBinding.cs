using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IPendingBinding{TService}"/>.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public class PendingBinding<TService> : IPendingBinding<TService> where TService : notnull
	{
		/// <inheritdoc cref="IPendingBinding{TService}.Applicator"/>
		public Action<IServiceBinding<TService>> Applicator { get; }

		/// <summary>
		/// 	Constructs an instance of <see cref="PendingBinding{TService}"/>.
		/// </summary>
		/// <param name="applicator">The function to set the binding.</param>
		public PendingBinding(Action<IServiceBinding<TService>> applicator)
		{
			Guard.Null(applicator, nameof(applicator));

			Applicator = applicator;
		}
	}
}
