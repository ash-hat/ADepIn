namespace Atlas
{
	/// <summary>
	/// 	Represents a service getter.
	/// </summary>
	public interface IServiceResolver
	{
		/// <summary>
		/// 	Tries to get a service. Returns <code>Some</code> if a binding was found, and <code>None</code> if it wasn't.
		/// </summary>
		/// <typeparam name="TService">The service to get.</typeparam>
		Option<TService> Get<TService>() where TService : notnull;
	}
}
