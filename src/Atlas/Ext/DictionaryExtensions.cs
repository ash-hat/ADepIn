using System;
using System.Collections.Generic;

namespace Atlas
{
	/// <summary>
	/// 	A collection of extension methods for <see cref="IDictionary{TKey,TValue}"/>.
	/// </summary>
	public static class DictionaryExtensions
	{
		/// <summary>
		/// 	If <paramref name="key"/> is not present in the dictionary, it is inserted with a value of <paramref name="default"/>.
		/// 	The value of the dictionary element is then returned.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="key">The key of the dictionary entry to get.</param>
		/// <param name="default">The default value of the dictionary entry.</param>
		/// <typeparam name="TKey">The type of the key of the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the value of the dictionary.</typeparam>
		public static TValue GetOrInsert<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue @default) where TKey : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(key, nameof(key));

			if (!@this.TryGetValue(key, out var value))
			{
				value = @default;
				@this.Add(key, value);
			}

			return value;
		}

		/// <summary>
		/// 	If <paramref name="key"/> is not present in the dictionary, it is inserted with the result of <paramref name="default"/>.
		/// 	The value of the dictionary element is then returned.
		/// 	<p>This is the lazily-evaluated variant of <seealso cref="GetOrInsert{TKey, TValue}"/>.</p>
		/// </summary>
		/// <param name="this"></param>
		/// <param name="key">The key of the dictionary entry to get.</param>
		/// <param name="default">The default value of the dictionary entry.</param>
		/// <typeparam name="TKey">The type of the key of the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the value of the dictionary.</typeparam>
		public static TValue GetOrInsertWith<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> @default) where TKey : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(key, nameof(key));

			if (!@this.TryGetValue(key, out var value))
			{
				value = @default();
				@this.Add(key, value);
			}

			return value;
		}

		/// <summary>
		/// 	Gets a dictionary value. If it does exist, returns Some, otherwise None.
		/// 	<p>This is the <see cref="Option{TValue}"/> compliant version of <see cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>.</p>
		/// </summary>
		/// <param name="this"></param>
		/// <param name="key">The key of the dictionary entry to get.</param>
		/// <typeparam name="TKey">The type of the key of the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the value of the dictionary.</typeparam>
		public static Option<TValue> OptionGetValue<TKey, TValue>(this
#if NET35
			IDictionary
#else
			IReadOnlyDictionary
#endif
			<TKey, TValue> @this, TKey key) where TKey : notnull
		{
			Guard.Null(@this, nameof(@this));
			Guard.Null(key, nameof(key));

#pragma warning disable 8600
			return @this.TryGetValue(key, out TValue value)
#pragma warning restore 8600
				? Option.Some(value)
				: Option.None<TValue>();
		}
	}
}
