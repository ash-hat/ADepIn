using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	A service binding which produces a service determined by a delegate.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce\.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public class FunctionalServiceBinding<TService, TContext> : IServiceBinding<TService, TContext>
		where TService : notnull
		where TContext : notnull
	{	
		private readonly WholeBindingImpl<TService, TContext> _impl;

		/// <summary>
		/// 	Constructs an instance of <see cref="FunctionalServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="impl">The full binding implementation to use when this binding is called.</param>
		public FunctionalServiceBinding(WholeBindingImpl<TService, TContext> impl)
		{
			Guard.Null(impl, nameof(impl));

			_impl = impl;
		}

		/// <summary>
		/// 	Constructs an instance of <see cref="FunctionalServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="impl">The recursive binding implementation to use when this binding is called.</param>
		public FunctionalServiceBinding(RecursiveBindingImpl<TService> impl)
		{
			Guard.Null(impl, nameof(impl));

			_impl = (services, _) => 
			{
				Guard.Null(services, nameof(services));

				return impl(services);
			};
		}

		/// <summary>
		/// 	Constructs an instance of <see cref="FunctionalServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="impl">The contextual binding implementation to use when this binding is called.</param>
		public FunctionalServiceBinding(ContextualBindingImpl<TService, TContext> impl)
		{
			Guard.Null(impl, nameof(impl));

			_impl = (_, context) =>
			{
				Guard.Null(context, nameof(context));

				return impl(context);
			};
		}

		/// <summary>
		/// 	Constructs an instance of <see cref="FunctionalServiceBinding{TService, TContext}"/>.
		/// </summary>
		/// <param name="impl">The pure binding implementation to use when this binding is called.</param>
		public FunctionalServiceBinding(PureBindingImpl<TService> impl)
		{
			Guard.Null(impl, nameof(impl));

			// For some reason, multiple lambda discards are only available in C# 9.0.
			_impl = (_0, _1) => impl();
		}

		/// <inheritdoc cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>
		public TService Get(IServiceResolver services, TContext context)
		{
			return _impl(services, context);
		}
	}
}
