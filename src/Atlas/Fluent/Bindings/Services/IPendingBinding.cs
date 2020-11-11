using System;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	Represent a binding not yet scoped and bound to a <see cref="IServiceBinder" />.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public interface IPendingBinding
#if NET35
<TService>
#else
<in TService>
#endif
		where TService : notnull
	{
		/// <summary>
		/// 	The function to set the binding.
		/// </summary>
		Action<IServiceBinding<TService>> Applicator { get; }
	}
}
