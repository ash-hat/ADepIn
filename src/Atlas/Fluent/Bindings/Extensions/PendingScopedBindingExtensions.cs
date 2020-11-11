using Atlas.Fluent.Impl;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IPendingScopedBinding{T}" />.
	/// </summary>
	public static class PendingScopedBindingExtensions
	{
		/// <summary>
		/// 	Binds this binding as a singleton service; only one instance will be retrieved and it will persist for each call to the binding.
		/// </summary>
		/// <typeparam name="TService">The type of the service to bind to.</typeparam>
		public static void InSingletonScope<TService>(this IPendingScopedBinding<TService> @this) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));

			@this.Applicator(new SingletonServiceBinding<TService>(@this.Binding));
		}

		/// <summary>
		/// 	Binds this binding as a transient service; a new instance will be retrieved for each call to the binding.
		/// </summary>
		/// <typeparam name="TService">The type of the service to bind to.</typeparam>
		public static void InTransientScope<TService>(this IPendingScopedBinding<TService> @this) where TService : notnull
		{
			Guard.Null(@this, nameof(@this));

			@this.Applicator(@this.Binding);
		}
	}
}
