using System;

namespace Atlas.Fluent
{
	/// <summary>
	/// 	A function that applies a binding to a binder.
	/// </summary>
	/// <param name="binding">The binding to apply.</param>
	/// <typeparam name="TService">The service the binding produces.</typeparam>
	/// <typeparam name="TContext">The context the binding consumes.</typeparam>
	/// <returns></returns>
	public delegate void PendingApplicator<in TService, out TContext>(IServiceBinding<TService, TContext> binding)
		where TService : notnull
		where TContext : notnull;

	/// <summary>
	/// 	Represent a binding not yet scoped and bound to a <see cref="IServiceBinder" />.
	/// </summary>
	/// <typeparam name="TService">The type of service to produce.</typeparam>
	/// <typeparam name="TContext">The type of context to consume.</typeparam>
	public interface IPendingBinding<in TService, out TContext>
		where TService : notnull
		where TContext : notnull
	{
		/// <summary>
		/// 	The function to set the binding.
		/// </summary>
		PendingApplicator<TService, TContext> Applicator { get; }
	}
}
