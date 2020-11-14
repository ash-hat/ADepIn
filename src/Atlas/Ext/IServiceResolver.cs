namespace Atlas
{
	/// <summary>
	///	 A collection of extension methods for <see cref="IServiceResolver"/>.
	/// </summary>
	public static class ExtIServiceResolver
	{
		/// <summary>
		/// 	Resolves a service using no context (<see cref="Unit"/>).
		/// </summary>
		/// <typeparam name="TService">The type of service to produce.</typeparam>
		public static Option<TService> Get<TService>(this IServiceResolver @this) where TService : notnull
		{
			return @this.Get<TService, Unit>(default);
		}
	}
}
