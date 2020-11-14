using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IScopedBindingStub{TService, TContext}"/>.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class ScopedBindingStub<TService, TContext> : IScopedBindingStub<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		/// <inheritdoc cref="IScopedBindingStub{TService, TContext}.Applicator"/>
		public StubApplicator<TService, TContext> Applicator { get; }

		/// <inheritdoc cref="IScopedBindingStub{TService, TContext}.Binding"/>
		public IServiceBinding<TService, TContext> Binding { get; }

		/// <summary>
		/// 	Constructs an instance of <see cref="ScopedBindingStub{TService, TContext}"/>.
		/// </summary>
		/// <param name="applicator">The function to set the binding.</param>
		/// <param name="binding">The binding, without scope.</param>
		public ScopedBindingStub(StubApplicator<TService, TContext> applicator, IServiceBinding<TService, TContext> binding)
		{
			Guard.Null(applicator, nameof(applicator));
			Guard.Null(binding, nameof(binding));

			Applicator = applicator;
			Binding = binding;
		}
	}
}
