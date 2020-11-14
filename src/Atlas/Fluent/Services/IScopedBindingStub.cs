namespace Atlas.Fluent
{
	/// <summary>
	/// 	Represents a binding that has been created and scoped but not yet bound to a <see cref="IServiceBinder" />.
	/// 	<p>This interface composites <see cref="IScopedBindingStub{TService, TContext}.Applicator"/> to improve ease of extension method usage.</p>
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public interface IScopedBindingStub<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		/// <summary>
		/// 	The function to set the binding.
		/// </summary>
		StubApplicator<TService, TContext> Applicator { get; }

		/// <summary>
		/// 	The binding awaiting to be bound.
		/// </summary>
		IServiceBinding<TService, TContext> Binding { get; }
	}
}
