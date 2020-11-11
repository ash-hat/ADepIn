namespace Atlas
{
	/// <summary>
	/// 	Represents a factory for a service implementation that may use pre-existing services; a binding between the service and implementation.
	/// </summary>
	/// <typeparam name="TService">The service to get.</typeparam>
	public interface IServiceBinding<out TService> where TService : notnull
	{
		/// <summary>
		/// 	Gets the service using the pre-existing services.
		/// </summary>
		/// <param name="services">The services available to be used by this binding.</param>
		TService Get(IServiceResolver services);
	}
}
