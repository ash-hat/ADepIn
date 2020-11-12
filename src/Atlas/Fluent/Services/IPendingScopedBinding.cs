namespace Atlas.Fluent
{
	/// <summary>
	/// 	Represents a binding that has been created and scoped but not yet bound to a <see cref="IServiceBinder" />.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public interface IPendingScopedBinding<TService, TContext> : IPendingBinding<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		/// <summary>
		/// 	The binding awaiting to be bound.
		/// </summary>
		IServiceBinding<TService, TContext> Binding { get; }
	}
}
