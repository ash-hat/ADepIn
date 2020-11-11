using System;

namespace Atlas
{
	/// <summary>
	/// 	Maps one value to another.
	/// 	Comparable to <see cref="Converter{TInput,TOutput}"/>, but with covariance and invariance
	/// </summary>
	/// <typeparam name="TSource">The type of the value.</typeparam>
	/// <typeparam name="TMapped">The type to map the value to.</typeparam>
	public delegate TMapped Mapper<in TSource, out TMapped>(TSource value);

	/// <summary>
	/// 	Maps one value to another, using a passed state.
	/// 	Comparable to <see cref="Converter{TInput,TOutput}"/>, but with covariance and invariance, and nullable attributes.
	/// </summary>
	/// <param name="value">The value to map.</param>
	/// <param name="state">The state to pass into the function.</param>
	/// <typeparam name="TSource">The type of the value.</typeparam>
	/// <typeparam name="TState">The type of the state to pass in.</typeparam>
	/// <typeparam name="TMapped">The type to map the value to.</typeparam>
	public delegate TMapped Mapper<in TSource, in TState, out TMapped>(TState state, TSource value)
		where TState : notnull;

	/// <summary>
	/// 	Checks the equality of two values, in a function form.
	/// </summary>
	/// <param name="a">The first value.</param>
	/// <param name="b">The second value.</param>
	/// <typeparam name="TValue">The type of both of the values.</typeparam>
	public delegate bool FunctionalEqualityComparer<in TValue>(TValue a, TValue b);
}
