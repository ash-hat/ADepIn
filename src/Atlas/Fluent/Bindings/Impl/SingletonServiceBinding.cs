namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service binding which calculates a value when first called, and outputs that value for every call.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public class SingletonServiceBinding<TService> : IServiceBinding<TService> where TService : notnull
	{
		private readonly IServiceBinding<TService> _binding;

		private Option<TService> _value;

		/// <summary>
		/// 	Constructs an instance of <see cref="SingletonServiceBinding{TService}"/>.
		/// </summary>
		/// <param name="binding">The binding, without scope.</param>
		public SingletonServiceBinding(IServiceBinding<TService> binding)
		{
			Guard.Null(binding, nameof(binding));

			_binding = binding;
			_value = Option.None<TService>();
		}

		/// <inheritdoc cref="IServiceBinding{TService}.Get"/>
		public TService Get(IServiceResolver services)
		{
			if (!_value.MatchSome(out var value))
			{
				Guard.Null(services, nameof(services));

				value = _binding.Get(services);
				_value.Replace(value);
			}

			return value;
		}
	}
}
