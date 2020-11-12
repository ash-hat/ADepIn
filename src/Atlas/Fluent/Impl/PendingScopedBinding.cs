using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IPendingScopedBinding{TService, TContext}"/>.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class PendingScopedBinding<TService, TContext> : IPendingScopedBinding<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		/// <inheritdoc cref="IPendingBinding{TService, TContext}.Applicator"/>
		public PendingApplicator<TService, TContext> Applicator { get; }

		/// <inheritdoc cref="IPendingScopedBinding{TService, TContext}.Binding"/>
		public IServiceBinding<TService, TContext> Binding { get; }

		/// <summary>
		/// 	Constructs an instance of <see cref="PendingScopedBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="applicator">The function to set the binding.</param>
		/// <param name="binding">The binding, without scope.</param>
		public PendingScopedBinding(PendingApplicator<TService, TContext> applicator, IServiceBinding<TService, TContext> binding)
		{
			Guard.Null(applicator, nameof(applicator));
			Guard.Null(binding, nameof(binding));

			Applicator = applicator;
			Binding = binding;
		}
	}
}
