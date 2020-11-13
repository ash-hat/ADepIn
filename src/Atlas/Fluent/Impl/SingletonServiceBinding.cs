namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service binding which calculates a value when first called, and outputs that value for every call.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class SingletonServiceBinding<TService, TContext> : IServiceBinding<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		private readonly IServiceBinding<TService, TContext> _binding;

		private Option<TService> _value;

		/// <summary>
		/// 	Constructs an instance of <see cref="SingletonServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="binding">The binding, without scope.</param>
		public SingletonServiceBinding(IServiceBinding<TService, TContext> binding)
		{
			Guard.Null(binding, nameof(binding));

			_binding = binding;
			_value = Option.None<TService>();
		}

		/// <inheritdoc cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>
		public TService Get(IServiceResolver services, TContext context)
		{
			if (!_value.MatchSome(out var value))
			{
				Guard.Null(services, nameof(services));
				Guard.Null(context, nameof(context));

				value = _binding.Get(services, context);
				_value.Replace(value);
			}

			return value;
		}
	}
}