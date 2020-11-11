namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	Gets a service, given a <see cref="IServiceResolver"/>.
	/// </summary>
	/// <param name="services">The services available to the getter.</param>
	/// <typeparam name="TService">The type of the service to get.</typeparam>
	public delegate TService ServiceGetter<out TService>(IServiceResolver services);

	/// <summary>
	/// 	A service binding which outputs a value determined by a function that is given access to existing services.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public class FunctionalResolverServiceBinding<TService> : IServiceBinding<TService> where TService : notnull
	{
		private readonly ServiceGetter<TService> _getter;

		/// <summary>
		/// 	Constructs an instance of <see cref="FunctionalResolverServiceBinding{TService}"/>.
		/// </summary>
		/// <param name="getter">The function to determine the service.</param>
		public FunctionalResolverServiceBinding(ServiceGetter<TService> getter)
		{
			Guard.Null(getter, nameof(getter));

			_getter = getter;
		}

		/// <inheritdoc cref="IServiceBinding{TService}.Get"/>
		public TService Get(IServiceResolver services)
		{
			Guard.Null(services, nameof(services));

			return _getter(services);
		}
	}
}
