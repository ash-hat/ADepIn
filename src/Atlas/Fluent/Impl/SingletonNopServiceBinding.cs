namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service binding which calculates a value when first called, and outputs that value for every call.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class SingletonNopServiceBinding<TService, TContext> : IServiceBinding<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		private readonly IServiceBinding<TService, TContext> _binding;

		private Option<Option<TService>> _value;

		/// <summary>
		/// 	Constructs an instance of <see cref="SingletonNopServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="binding">The binding, without scope.</param>
		public SingletonNopServiceBinding(IServiceBinding<TService, TContext> binding)
		{
			Guard.Null(binding, nameof(binding));

			_binding = binding;
			_value = Option.None<Option<TService>>();
		}

		/// <inheritdoc cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>
		public Option<TService> Get(IServiceResolver services, TContext context)
		{
			return _value.UnwrapOrElse(() =>
			{
				Guard.Null(services, nameof(services));

				var value = _binding.Get(services, context);
				_value.Replace(value);

				return value;
			});
		}
	}
}
