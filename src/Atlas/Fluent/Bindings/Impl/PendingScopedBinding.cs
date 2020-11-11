using System;

namespace Atlas.Fluent.Impl
{
	/// <summary>
	/// 	An implementation of <see cref="IPendingScopedBinding{TService}"/>.
	/// </summary>
	/// <typeparam name="TService">The type of the service to bind to.</typeparam>
	public class PendingScopedBinding<TService> : IPendingScopedBinding<TService> where TService : notnull
	{
		/// <inheritdoc cref="IPendingBinding{TService}.Applicator"/>
		public Action<IServiceBinding<TService>> Applicator { get; }

		/// <inheritdoc cref="IPendingScopedBinding{TService}.Binding"/>
		public IServiceBinding<TService> Binding { get; }

		/// <summary>
		/// 	Constructs an instance of <see cref="PendingScopedBinding{TService}"/>.
		/// </summary>
		/// <param name="applicator">The function to set the binding.</param>
		/// <param name="binding">The binding, without scope.</param>
		public PendingScopedBinding(Action<IServiceBinding<TService>> applicator, IServiceBinding<TService> binding)
		{
			Guard.Null(applicator, nameof(applicator));
			Guard.Null(binding, nameof(binding));

			Applicator = applicator;
			Binding = binding;
		}
	}
}
