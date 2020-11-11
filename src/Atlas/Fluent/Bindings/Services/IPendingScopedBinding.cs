namespace Atlas.Fluent
{
	/// <summary>
	/// 	Represents a binding that has been created and scoped but not yet bound to a <see cref="IServiceBinder" />.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public interface IPendingScopedBinding<TService> : IPendingBinding<TService> where TService : notnull
	{
		/// <summary>
		/// 	The binding awaiting to be bound.
		/// </summary>
		IServiceBinding<TService> Binding { get; }
	}
}
