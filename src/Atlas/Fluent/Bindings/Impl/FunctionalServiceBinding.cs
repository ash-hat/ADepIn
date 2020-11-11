using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service binding which outputs a value determined by a delegate.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public class FunctionalServiceBinding<TService> : IServiceBinding<TService> where TService : notnull
	{
		private readonly Func<TService> _getter;

		/// <summary>
		/// 	Constructs an instance of <see cref="FunctionalServiceBinding{TService}"/>.
		/// </summary>
		/// <param name="getter">The function to determine the service.</param>
		public FunctionalServiceBinding(Func<TService> getter)
		{
			Guard.Null(getter, nameof(getter));

			_getter = getter;
		}

		/// <inheritdoc cref="IServiceBinding{TService}.Get"/>
		public TService Get(IServiceResolver services)
		{
			return _getter();
		}
	}
}
