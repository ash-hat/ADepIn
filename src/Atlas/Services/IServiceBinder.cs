namespace Atlas
{
	/// <summary>
	/// 	Represents a service setter.
	/// </summary>
	public interface IServiceBinder
	{
		/// <summary>
		/// 	Sets a binding for a service.
		/// </summary>
		/// <param name="binding">The binding for the service.</param>
		/// <typeparam name="TService">The service to assign the binding to.</typeparam>
		void Bind<TService>(IServiceBinding<TService> binding) where TService : notnull;
	}
}
