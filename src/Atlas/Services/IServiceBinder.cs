namespace Atlas
{
	/// <summary>
	/// 	Represents a service setter.
	/// </summary>
	public interface IServiceBinder
	{
		/// <summary>
		/// 	Assigns a binding to a service.
		/// </summary>
		/// <param name="binding">The binding for the service.</param>
		/// <typeparam name="TService">The service to assign the binding to.</typeparam>
		/// <typeparam name="TContext">The context the binding uses.</typeparam>
		void Bind<TService, TContext>(IServiceBinding<TService, TContext> binding)
			where TService : notnull
			where TContext : notnull;
	}
}
