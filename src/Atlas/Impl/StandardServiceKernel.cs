using System;
using System.Collections.Generic;
using System.Linq;

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
		/// <exception cref="ArgumentOutOfRangeException">The inner value of <paramref name="value" /> was less than 0.</exception>
		public Option<int> MaxRecursion
		{
			get => _maxRecursion;
			set => _maxRecursion = value.Map(v =>
			{
				if (v < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), v, "Value was less than 0.");
				}

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

			TService Factory(IServiceKernel kernel, TContext context)
			{
				var service = binding.Get(kernel, context);

				if (Nullability<TService>.IsNull(service))
				{
					throw new InvalidOperationException($"{binding} returned a null implementation.");
				}

				return service;
			}

			_infos.Add(typeof(ServiceInfo<TService, TContext>), new ServiceInfo<TService, TContext>(Factory));
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
					if (_maxRecursion.Contains(info.ResolutionStackSize))
					{
						throw new InvalidOperationException($"Detected recursive resolution while resolving {typeof(TService)}.");
					}

					++info.ResolutionStackSize;

					try
					{
						return info.Factory(this, context);
					}
					finally
					{
						--info.ResolutionStackSize;
					}
				});
		}

        private class ServiceInfo<TService, TContext>
			where TService : notnull
			where TContext : notnull
        {
			public delegate TService FactoryHandler(IServiceKernel kernel, TContext context);

            public FactoryHandler Factory { get; }

            public int ResolutionStackSize { get; set; }

			public ServiceInfo(FactoryHandler factory)
			{
				Factory = factory;

				ResolutionStackSize = 0;
			}
        }
	}
}
