using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Atlas
{
	/// <summary>
	/// 	Constructors for the <see cref="Option{T}"/> discriminated union.
	/// </summary>
	public static class Option
	{
		/// <summary>
		/// 	Constructs a <code>Some</code>, which contains an inner value.
		/// </summary>
		/// <param name="value">The inner value.</param>
		/// <typeparam name="T">The type of the inner value (<paramref name="value"/>).</typeparam>
		public static Option<T> Some<T>(T value)
		{
			return new Option<T>(true, value);
		}

		/// <summary>
		/// 	Constructs a <code>None</code>.
		/// </summary>
		/// <typeparam name="T">The type of the would-be inner value.</typeparam>
		public static Option<T> None<T>()
		{
			return new Option<T>(false, default);
		}
	}

	/// <summary>
	/// 	Extension methods related to the <see cref="Option{T}"/> discriminated union.
	/// </summary>
	public static class OptionExtensions
	{
		/// <summary>
		/// 	Combines the inner and outer options into a singular <see cref="Option{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static Option<T> Flatten<T>(this Option<Option<T>> @this)
		{
			return @this.MatchSome(out var inner) ? inner : Option.None<T>();
		}

		/// <summary>
		/// 	Checks if there is an inner value and if it equates to the provided value.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value">The value to compare equality to.</param>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static bool Contains<T>(this Option<T> @this, T value) where T : IEquatable<T>
		{
			Guard.Null(value, nameof(value));

			return @this.Contains(value, (a, b) => a.Equals(b));
		}

		/// <summary>
		/// 	Checks if there is an inner value and if it equates to the provided value.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value">The value to compare equality to.</param>
		/// <param name="comparer">The comparer to check equality between the inner value and other value.</param>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static bool Contains<T>(this Option<T> @this, T value, IEqualityComparer<T> comparer)
		{
			Guard.Null(value, nameof(value));
			Guard.Null(comparer, nameof(comparer));

			return @this.Contains(value, comparer.Equals);
		}

		/// <summary>
		/// 	Checks if there is an inner value and if it equates to the provided value.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value">The value to compare equality to.</param>
		/// <param name="comparer">The comparer to check equality between the inner value and other value.</param>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static bool Contains<T>(this Option<T> @this, T value, FunctionalEqualityComparer<T> comparer)
		{
			Guard.Null(value, nameof(value));
			Guard.Null(comparer, nameof(comparer));

			return @this.MatchSome(out var thisValue) && comparer(thisValue, value);
		}

		/// <summary>
		/// 	Compares the equality of self and another <see cref="Option{T}"/>.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="other">The option to compare equality with.</param>
		/// <typeparam name="T">The type of the inner values.</typeparam>
		public static bool Matches<T>(this Option<T> @this, Option<T> other) where T : IEquatable<T>
		{
			return @this.Matches(other, (a, b) => a.Equals(b));
		}

		/// <summary>
		/// 	Compares the equality of self and another <see cref="Option{T}"/>.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="other">The option to compare equality with.</param>
		/// <param name="comparer">The comparer to check equality between the inner value and other value.</param>
		/// <typeparam name="T">The type of the inner values.</typeparam>
		public static bool Matches<T>(this Option<T> @this, Option<T> other, IEqualityComparer<T> comparer)
		{
			Guard.Null(comparer, nameof(comparer));

			return @this.Matches(other, comparer.Equals);
		}

		/// <summary>
		/// 	Checks the equality of self and another <see cref="Option{T}"/>.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="other">The option to compare equality with.</param>
		/// <param name="comparer">The comparer to check equality between the inner value and other value.</param>
		/// <typeparam name="T">The type of the inner values.</typeparam>
		public static bool Matches<T>(this Option<T> @this, Option<T> other, FunctionalEqualityComparer<T> comparer)
		{
			Guard.Null(comparer, nameof(comparer));

			return other.MatchSome(out var value) && @this.Contains(value, comparer);
		}

		/// <summary>
		/// 	Sets self to <code>None</code> and returns the previous value of self.
		/// </summary>
		/// <param name="this"></param>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static Option<T> Take<T>(this ref Option<T> @this)
		{
			var copy = @this;

			@this = Option.None<T>();

			return copy;
		}

		/// <summary>
		/// 	Sets self to <code>Some(<paramref name="value"/>)</code> and returns the previous value of self.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value">The new inner value.</param>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static Option<T> Replace<T>(this ref Option<T> @this, T value)
		{
			var copy = @this;

			@this = Option.Some(value);

			return copy;
		}

		/// <summary>
		/// 	Sets self to <code>Some(<paramref name="value"/>)</code> if it was not <code>Some</code> already, and returns the inner value.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value">The inner value if one is not present.</param>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static T GetOrInsert<T>(this ref Option<T> @this, T value)
		{
			if (@this.MatchSome(out var inner))
			{
				return inner;
			}

			@this = Option.Some(value);

			return value;
		}

		/// <summary>
		/// 	Sets self to <code>Some(<paramref name="value"/>())</code> if it was not <code>Some</code> already, and returns the inner value.
		/// 	<p>This is the lazily-evaluated variant of <seealso cref="GetOrInsert{T}"/>.</p>
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value">The getter to invoke if an inner value is not present.</param>
		/// <typeparam name="T">The type of the inner value.</typeparam>
		public static T GetOrInsertWith<T>(this ref Option<T> @this, Func<T> value)
		{
			if (@this.MatchSome(out var inner))
			{
				return inner;
			}

			var realized = value();
			@this = Option.Some(realized);

			return realized;
		}

		/// <summary>
	 	/// 	Simultaneously filters and projects a sequence of elements onto a new form.
	 	/// 	<p>This is equivalent to a <see cref="Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, int, TResult})"/> call chained into a <see cref="Enumerable.Where{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>.</p>
	 	/// </summary>
	 	/// <param name="this"></param>
	 	/// <param name="selector">Projects a <typeparamref name="TSource"/> onto an <see cref="Option{TResult}"/>.</param>
	 	/// <typeparam name="TSource">The type of the elements of the source.</typeparam>
	 	/// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/>.</typeparam>
	 	public static IEnumerable<TResult> WhereSelect<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, Option<TResult>> selector)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			Guard.Null(@this, nameof(@this));
			Guard.Null(selector, nameof(selector));

			// ReSharper disable once PossibleMultipleEnumeration
			return @this.Select(selector).WhereSome();
		}

		/// <summary>
		/// 	Projects a sequence of <see cref="Option{T}"/> into the inner values of the <code>Some</code>s in the sequence.
		/// </summary>
		/// <typeparam name="T">The type of the inner values.</typeparam>
		public static IEnumerable<T> WhereSome<T>(this IEnumerable<Option<T>> @this)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			Guard.Null(@this, nameof(@this));

			// ReSharper disable once PossibleMultipleEnumeration
			foreach (var item in @this)
			{
				if (!item.MatchSome(out var value))
				{
					continue;
				}

				yield return value;
			}
		}
	}

	/// <summary>
	///		A discriminated (binary) union that represents an optional, non-nullable value.
	/// 	<p>If no value is present, it is <code>None</code>, otherwise is it <code>Some</code>.</p>
	/// 	<p>This is not a replacement for <see cref="Nullable{T}" /> and nullable reference types, designed to unify both.</p>
	/// </summary>
	/// <typeparam name="T">The type of the value contained.</typeparam>
	public readonly struct Option<T>
	{
		private readonly bool _isSome;
		[AllowNull]
		[MaybeNull]
		private readonly T _value;

		/// <summary>
		/// 	Whether or not a value is present. Opposite of <seealso cref="IsNone"/>.
		/// 	<p>If you are going to access the value afterward, use <seealso cref="MatchSome"/> to do so atomically.</p>
		/// </summary>
		public bool IsSome => _isSome;
		/// <summary>
		/// 	Whether or not a value is not present. Opposite of <seealso cref="IsSome"/>.
		/// </summary>
		public bool IsNone => !_isSome;

		internal Option(bool isSome, [AllowNull] T value)
		{
			_isSome = isSome;
			_value = value;
		}

		/// <summary>
		/// 	Returns <code>true</code> with <code>v</code> if <code>Some(v)</code>, otherwise returns <code>false</code>.
		/// </summary>
		/// <param name="value">The value, if the return value is <code>true</code>.</param>
		public bool MatchSome([MaybeNullWhen(false)] out T value)
		{
			value = _value!;
			return IsSome;
		}

		/// <summary>
		/// 	Returns <code>v</code> if <code>Some(v)</code>, otherwise throws an <see cref="InvalidOperationException"/> with the message provided by <paramref name="message"/>.
		/// 	<p>Opposite of <seealso cref="ExpectNone"/>.</p>
		/// </summary>
		/// <param name="message">The message to throw if <code>None</code>.</param>
		public T Expect(string message)
		{
			Guard.Null(message, nameof(message));

			if (MatchSome(out var some))
			{
				return some;
			}

			throw new InvalidOperationException(message);
		}

		/// <summary>
		/// 	Throws an <see cref="InvalidOperationException"/> with the message provided by <paramref name="message"/> if <code>Some</code>, otherwise does nothing.
		/// 	<p>Opposite of <seealso cref="Expect"/>.</p>
		/// </summary>
		/// <param name="message">The message to throw if <code>Some</code>.</param>
		public void ExpectNone(string message)
		{
			Guard.Null(message, nameof(message));

			if (IsNone)
			{
				return;
			}

			throw new InvalidOperationException(message);
		}

		/// <summary>
		/// 	Returns <code>v</code> if <code>Some(v)</code>, otherwise throws an <see cref="InvalidOperationException"/>.
		/// 	Shortcut for <seealso cref="Expect"/>. Opposite of <seealso cref="UnwrapNone"/>.
		/// </summary>
		public T Unwrap()
		{
			return Expect("Expected a Some option when unwrapping " + typeof(Option<T>) + ".");
		}

		/// <summary>
		/// 	If <code>Some</code>, throws an <see cref="InvalidOperationException"/>
		/// 	Shortcut for <seealso cref="ExpectNone"/>. Opposite of <seealso cref="Unwrap"/>.
		/// </summary>
		public void UnwrapNone()
		{
			ExpectNone("Expected a None option when unwrapping " + typeof(Option<T>) + ".");
		}

		/// <summary>
		/// 	Returns <code>v</code> if <code>Some(v)</code>, otherwise returns <paramref name="default"/>.
		/// </summary>
		/// <param name="default">The value to return if <code>None</code>.</param>
		public T UnwrapOr(T @default)
		{
			return MatchSome(out var value) ? value : @default;
		}

		/// <summary>
		/// 	Returns <code>v</code> if <code>Some(v)</code>, otherwise returns the result of <paramref name="default"/>.
		/// 	<p>This is the lazily-evaluated variant of <seealso cref="UnwrapOr(T)"/>.</p>
		/// </summary>
		/// <param name="default">The getter to invoke if an inner value is not present.</param>
		public T UnwrapOrElse(Func<T> @default)
		{
			Guard.Null(@default, nameof(@default));

			return MatchSome(out var value) ? value : @default();
		}

		/// <summary>
		/// 	Returns self if <code>Some</code>, otherwise <paramref name="other"/>.
		/// 	<p>Opposite of <seealso cref="And"/>.</p>
		/// </summary>
		/// <param name="other">The option to return if <code>None</code>.</param>
		public Option<T> Or(Option<T> other)
		{
			return MatchSome(out var value) ? Option.Some(value) : other;
		}

		/// <summary>
		/// 	Returns self if <code>Some</code>, otherwise <paramref name="other"/>.
		/// 	<p>This is the lazily-evaluated variant of <seealso cref="Or(Option{T})"/>.</p>
		/// </summary>
		/// <param name="other">The getter to invoke if <code>None</code>.</param>
		public Option<T> OrElse(Func<Option<T>> other)
		{
			return MatchSome(out var value) ? Option.Some(value) : other();
		}

		/// <summary>
		/// 	Returns <paramref name="other"/> if <code>Some</code>, otherwise <code>None</code>.
		/// 	Opposite of <seealso cref="Or"/>.
		/// </summary>
		/// <param name="other">The option to return if <code>Some</code>.</param>
		public Option<T> And(Option<T> other)
		{
			return IsNone ? Option.None<T>() : other;
		}

		/// <summary>
		/// 	Returns self or <paramref name="other"/> if only one is <code>Some</code>, otherwise returns <code>None</code>.
		/// </summary>
		/// <param name="other">The option to return if it is <code>Some</code> and self is <code>None</code>.</param>
		public Option<T> Xor(Option<T> other)
		{
			T value;

			if (MatchSome(out value)) // self is Some
			{
				if (other.IsNone)
				{
					return Option.Some(value);
				}
			}
			else // self is None
			{
				if (other.MatchSome(out value))
				{
					return Option.Some(value);
				}
			}

			return Option.None<T>();
		}

		/// <summary>
		/// 	Returns <code>Some(v)</code> if <code>Some(v)</code> and <code><paramref name="predicate"/>(v)</code>, otherwise returns <code>None</code>.
		/// </summary>
		/// <param name="predicate">The function that determines if <code>Some(x)</code> remains <code>Some</code> (<see langword="true"/>) or turns into <code>None</code> (<see langword="false"/>).</param>
		public Option<T> Filter(Func<T, bool> predicate)
		{
			Guard.Null(predicate, nameof(predicate));

			return MatchSome(out var value) && predicate(value)
				? Option.Some(value)
				: Option.None<T>();
		}

		/// <summary>
		/// 	Returns <code>Some(<paramref name="mapper"/>(v))</code> if <code>Some(v)</code>, otherwise returns <code>None</code>.
		/// </summary>
		/// <param name="mapper">The function to mutate <code>v</code> if <code>Some(v)</code>.</param>
		/// <typeparam name="TMapped">The type to map the inner value to.</typeparam>
		public Option<TMapped> Map<TMapped>(Mapper<T, TMapped> mapper)
		{
			Guard.Null(mapper, nameof(mapper));

			return MatchSome(out var value) ? Option.Some(mapper(value)) : Option.None<TMapped>();
		}

		/// <summary>
		/// 	Returns <code>Some(<paramref name="mapper"/>(v))</code> if <code>Some(v)</code>, otherwise returns <code>Some(<paramref name="default"/>)</code>.
		/// 	<p>Compound of <seealso cref="Map{TMapped}(Mapper{T, TMapped})"/> and <seealso cref="Or"/>.</p>
		/// </summary>
		/// <param name="default">The value to return if <code>None</code>.</param>
		/// <param name="mapper">The function to mutate <code>v</code> if <code>Some(v)</code>.</param>
		/// <typeparam name="TMapped">The type to map the inner value to.</typeparam>
		public TMapped MapOr<TMapped>(TMapped @default, Mapper<T, TMapped> mapper)
		{
			Guard.Null(mapper, nameof(mapper));

			return MatchSome(out var value) ? mapper(value) : @default;
		}

		/// <summary>
		/// 	Returns <code>Some(<paramref name="mapper"/>(v))</code> if <code>Some(v)</code>, otherwise returns <code>Some(<paramref name="default"/>())</code>.
		/// 	<p>This is the lazily-evaluated variant of <seealso cref="MapOr"/>.</p>
		/// </summary>
		/// <param name="default">The getter to invoke if <code>None</code>.</param>
		/// <param name="mapper">The function to mutate <code>v</code> if <code>Some(v)</code>.</param>
		/// <typeparam name="TMapped">The type to map the inner value to.</typeparam>
		public TMapped MapOrElse<TMapped>(Func<TMapped> @default, Mapper<T, TMapped> mapper)
		{
			Guard.Null(@default, nameof(@default));
			Guard.Null(mapper, nameof(mapper));

			return MatchSome(out var value) ? mapper(value) : @default();
		}

		/// <summary>
		/// 	Returns "Some(<code>value</code>)" if <code>Some</code>, otherwise (<code>None</code>) returns "None".
		/// </summary>
		public override string ToString()
		{
			return MatchSome(out var value)
				? "Some(" + value + ")"
				: "None";
		}
	}
}
