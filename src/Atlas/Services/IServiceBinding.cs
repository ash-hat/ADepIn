namespace Atlas
{
	/// <summary>
	/// 	Represents a service implementation.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public interface IServiceBinding<TService, in TContext> 
		where TService : notnull
		where TContext : notnull
	{
		/// <summary>
		/// 	Gets the service this binding represents, given other services and contextual information.
		/// </summary>
		/// <param name="services">The services available in this service request.</param>
		/// <param name="context">Information about the service request.</param>
		Option<TService> Get(IServiceResolver services, TContext context);
	}
}
