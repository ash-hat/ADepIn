using System.Collections.Generic;

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
		private readonly Dictionary<TContext, Option<TService>> _values;

		/// <summary>
		/// 	Constructs an instance of <see cref="SingletonNopServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="binding">The binding, without scope.</param>
		public SingletonServiceBinding(IServiceBinding<TService, TContext> binding)
		{
			Guard.Null(binding, nameof(binding));

			_binding = binding;
			_values = new Dictionary<TContext, Option<TService>>();
		}

		/// <inheritdoc cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>
		public Option<TService> Get(IServiceResolver services, TContext context)
		{
			return _values.OptionGetValue(context).UnwrapOrElse(() =>
			{
				Guard.Null(services, nameof(services));
				Guard.Null(context, nameof(context));

				var value = _binding.Get(services, context);
				_values.Add(context, value);

				return value;
			});
		}
	}
}
