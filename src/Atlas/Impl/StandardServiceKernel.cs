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
		private readonly Dictionary<Type, object> _bindings;
		private readonly Dictionary<Type, object> _observers;
		private readonly Dictionary<Type, int> _currentlyGetting;

		private Option<int> _maxRecursion;

		/// <summary>
		/// 	The maximum amount of times to recursively get a service. If a service is gotten more than this many times within the same call, an exception is thrown.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The inner value of <paramref name="value" /> was less than <code>0</code>.</exception>
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
			_bindings = new Dictionary<Type, object>();
			_observers = new Dictionary<Type, object>();
			_currentlyGetting = new Dictionary<Type, int>();

			_maxRecursion = Option.Some(1000);
		}

		private Func<TService> GetServiceHandle<TService>(IServiceBinding<TService> binding) where TService : notnull
		{
			return () =>
			{
				var typed = (IServiceBinding<TService>) binding;
				var service = typed.Get(this);

				if (Nullability<TService>.IsNull(service))
				{
					throw new InvalidOperationException($"{typed} returned a null service.");
				}

				return service;
			};
		}

		/// <inheritdoc cref="IServiceBinder.Bind{TService}" />
		public void Bind<TService>(IServiceBinding<TService> binding) where TService : notnull
		{
			Guard.Null(binding, nameof(binding));

			_bindings.Add(typeof(TService), binding);

			var info = (ObserverInfo<TService>) _observers.GetOrInsertWith(typeof(TService), () => new ObserverInfo<TService>());
			var handle = GetServiceHandle(binding);
			info.Handle.Replace(handle);

			if (info.Observers.Take().MatchSome(out var observers))
			{
				foreach (IServiceObserver<TService> observer in observers)
				{
					observer.Notify(this, handle);
				}
			}
		}

		/// <inheritdoc cref="IServiceResolver.Get{TService}" />
		public Option<TService> Get<TService>() where TService : notnull
		{
			if (_maxRecursion.MatchSome(out var maxRecursion) && _currentlyGetting.TryGetValue(typeof(TService), out var count) &&
				count >= maxRecursion)
			{
				throw new InvalidOperationException($"Recursive path detected while getting {typeof(TService)}.");
			}

			_currentlyGetting[typeof(TService)] = _currentlyGetting.GetOrInsert(typeof(TService), 0) + 1;

			try
			{
				return _bindings.OptionGetValue(typeof(TService))
					.Map(this, (x, @this) => ((IServiceBinding<TService>) x).Get(@this));
			}
			finally
			{
				--_currentlyGetting[typeof(TService)];
			}
		}

		/// <inheritdoc cref="IObservableServiceResolver.Observe{TService}" />
		public void Observe<TService>(IServiceObserver<TService> observer) where TService : notnull
		{
			Guard.Null(observer, nameof(observer));

			var info = (ObserverInfo<TService>) _observers.GetOrInsertWith(typeof(TService), () => new ObserverInfo<TService>());
			if (info.Handle.MatchSome(out var handle))
			{
				observer.Notify(this, handle);
			}
			else
			{
				var observers = info.Observers.GetOrInsertWith(() => new HashSet<IServiceObserver<TService>>());
				
				observers.Add(observer);
			}
		}

	 	private class ObserverInfo<TService> where TService : notnull
	 	{
			public Option<HashSet<IServiceObserver<TService>>> Observers;
			public Option<Func<TService>> Handle;

			public ObserverInfo()
			{
				Observers = Option.None<HashSet<IServiceObserver<TService>>>();
				Handle = Option.None<Func<TService>>();
			}
	 	}
	}
}
