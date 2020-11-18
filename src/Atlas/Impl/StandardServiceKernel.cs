using System;
using System.Collections.Generic;

namespace Atlas.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IServiceKernel" /> with recursive path detection.
	/// </summary>
	public class StandardServiceKernel : IServiceKernel
	{
		private readonly Dictionary<Type, object> _infos;

		private Option<int> _maxRecursion;

		/// <summary>
		/// 	The maximum amount of times to recursively get a service. If a service is gotten more than this many times within the same call, an exception is thrown.
		/// </summary>
		public Option<int> MaxRecursion
		{
			get => _maxRecursion;
			set => _maxRecursion = value.Map(v =>
			{
				Guard.Negative(v, nameof(value));

				return v;
			});
		}

		/// <summary>
		/// 	Creates an instance of <see cref="StandardServiceKernel" />.
		/// </summary>
		public StandardServiceKernel()
		{
			_infos = new Dictionary<Type, object>();

			_maxRecursion = Option.Some(1000);
		}

		/// <inheritdoc cref="IServiceBinder.Bind{TService, TContext}" />
		public void Bind<TService, TContext>(IServiceBinding<TService, TContext> binding)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(binding, nameof(binding));

			_infos.Add(typeof(ServiceInfo<TService, TContext>), new ServiceInfo<TService, TContext>(binding));
		}

		/// <inheritdoc cref="IServiceResolver.Get{TService, TContext}" />
		public Option<TService> Get<TService, TContext>(TContext context)
			where TService : notnull
			where TContext : notnull
		{
			Guard.Null(context, nameof(context));

			return _infos.OptionGetValue(typeof(ServiceInfo<TService, TContext>))
				.Map(x => (ServiceInfo<TService, TContext>) x)
				.Map(info =>
				{
					if (_maxRecursion.MatchSome(out var limit) && info.ResolutionStackSize > limit)
					{
						throw new InvalidOperationException($"Detected recursive resolution while resolving {typeof(TService)}.");
					}

					++info.ResolutionStackSize;

					try
					{
						return info.Binding.Get(this, context);
					}
					finally
					{
						--info.ResolutionStackSize;
					}
				})
				.Flatten();
		}

		private class ServiceInfo<TService, TContext>
			where TService : notnull
			where TContext : notnull
		{
			public readonly IServiceBinding<TService, TContext> Binding;

			public int ResolutionStackSize;

			public ServiceInfo(IServiceBinding<TService, TContext> binding)
			{
				Binding = binding;

				ResolutionStackSize = 0;
			}
		}
	}
}
