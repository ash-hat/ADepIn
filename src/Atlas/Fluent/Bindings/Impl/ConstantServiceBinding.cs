namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service binding which outputs a constant value, determined before binding.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public class ConstantServiceBinding<TService> : IServiceBinding<TService> where TService : notnull
	{
		private readonly TService _value;

		/// <summary>
		/// 	Constructs an instance of <see cref="ConstantServiceBinding{TService}"/>.
		/// </summary>
		/// <param name="value">The constant value of the service.</param>
		public ConstantServiceBinding(TService value)
		{
			Guard.Null(value, nameof(value));

			_value = value;
		}

		/// <inheritdoc cref="IServiceBinding{TService}.Get"/>
		public TService Get(IServiceResolver services)
		{
			return _value;
		}
	}
}
