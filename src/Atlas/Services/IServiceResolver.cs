namespace Atlas
{
	/// <summary>
	/// 	Represents a service getter.
	/// </summary>
	public interface IServiceResolver
	{
		/// <summary>
		/// 	Resolves a service using a context.
		/// </summary>
		/// <param name="context">The context to fetch the service with.</param>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		/// <typeparam name="TContext">The type of context to consume.</typeparam>
		Option<TService> Get<TService, TContext>(TContext context) 
			where TService : notnull 
			where TContext : notnull;
	}
}
