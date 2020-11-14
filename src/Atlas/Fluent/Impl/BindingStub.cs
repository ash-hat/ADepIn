namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IBindingStub{TService, TContext}"/>.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class BindingStub<TService, TContext> : IBindingStub<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		/// <inheritdoc cref="IBindingStub{TService, TContext}.Applicator"/>
		public StubApplicator<TService, TContext> Applicator { get; }

		/// <summary>
		/// 	Constructs an instance of <see cref="BindingStub{TService, TContext}"/>.
		/// </summary>
		/// <param name="applicator">The function to set the binding.</param>
		public BindingStub(StubApplicator<TService, TContext> applicator)
		{
			Guard.Null(applicator, nameof(applicator));

			Applicator = applicator;
		}
	}
}
