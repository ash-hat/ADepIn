namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service binding which outputs a constant value, determined before binding.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce\.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class ConstantServiceBinding<TService, TContext> : IServiceBinding<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{
		private readonly TService _value;

		/// <summary>
		/// 	Constructs an instance of <see cref="ConstantServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="value">The constant value of the service.</param>
		public ConstantServiceBinding(TService value)
		{
			Guard.Null(value, nameof(value));

			_value = value;
		}

		/// <inheritdoc cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>
		public TService Get(IServiceResolver services, TContext context)
		{
			return _value;
		}
	}
}
