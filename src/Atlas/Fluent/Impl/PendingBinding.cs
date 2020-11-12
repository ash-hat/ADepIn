using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IPendingBinding{TService, TContext}"/>.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class PendingBinding<TService, TContext> : IPendingBinding<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		/// <inheritdoc cref="IPendingBinding{TService, TContext}.Applicator"/>
		public PendingApplicator<TService, TContext> Applicator { get; }

		/// <summary>
		/// 	Constructs an instance of <see cref="PendingBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="applicator">The function to set the binding.</param>
		public PendingBinding(PendingApplicator<TService, TContext> applicator)
		{
			Guard.Null(applicator, nameof(applicator));

			Applicator = applicator;
		}
	}
}
