using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ADepIn
{
	/// <summary>
	/// 	A tool to be called at the beginning of a method to guard against bad input.
	/// </summary>
	public static class Guard
	{
		/// <summary>
		/// 	Throws an exception if an element of the parameter is <see langword="null" />.
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <typeparam name="T">The type of the items in the parameter.</typeparam>
		public static IEnumerable<T> NullElement<T>(IEnumerable<T> value, string name)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			Null(value, nameof(value));
			Null(name, nameof(name));

			return NullElementInternal(value, name);
		}

		/// <summary>
		/// 	Throws an indexed exception if an element of the parameter is <see langword="null" />.
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <typeparam name="T">The type of the items in the parameter.</typeparam>
		public static void NullIndex<T>(T[] value, string name)
		{
			Null(value, nameof(value));
			Null(name, nameof(name));

			NullIndexInternal(value, name, x => x.Length, (x, i) => x[i]);
		}

		/// <summary>
		/// 	Throws an indexed exception if an element of the parameter is <see langword="null" />.
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <typeparam name="T">The type of the items in the parameter.</typeparam>
		public static void NullIndex<T>(IList<T> value, string name)
		{
			Null(value, nameof(value));
			Null(name, nameof(name));

			NullIndexInternal(value, name, x => x.Count, (x, i) => x[i]);
		}

#if !NET35
		/// <summary>
		/// 	Throws an indexed exception if an element of the parameter is <see langword="null" />.
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <typeparam name="T">The type of the items in the parameter.</typeparam>
		public static void NullIndex<T>(IReadOnlyList<T> value, string name)
		{
			Null(value, nameof(value));
			Null(name, nameof(name));

			NullIndexInternal(value, name, x => x.Count, (x, i) => x[i]);
		}
#endif

		/// <summary>
		/// 	Throws an exception if the parameter is <see langword="null" />.
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		public static void Null(object? value, string name)
		{
			// Do not replace this with a NullParameter call. It will be recursive. I did not accidentally do this.
			if (name is null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (value is null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>
		/// 	Throws an exception if the parameter is <see langword="null" /> or whitespace (includes empty).
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		public static void NullOrWhiteSpace(string? value, string name)
		{
			Null(name, nameof(name));
			Null(value, name);

			var isWhitespace =
#if !NET35
				string.IsNullOrWhiteSpace(value!)
#else
				value!.Trim() == string.Empty
#endif
				;
			if (isWhitespace)
			{
				throw new ArgumentException("Value cannot be whitespace.", name);
			}
		}

		/// <summary>
		/// 	Throws an exception if the parameter is negative.
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		public static void Negative(int value, string name)
		{
			Null(name, nameof(name));

			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(name, value, "Value cannot be negative.");
			}
		}

		/// <summary>
		/// 	Throws an exception if the parameter is negative or zero.
		/// </summary>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		public static void NonPositive(int value, string name)
		{
			Null(name, nameof(name));

			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(name, value, "Value cannot be negative or zero.");
			}
		}

		/// <summary>
		/// 	Throws an exception if the index is out of bounds of the array.
		/// </summary>
		/// <param name="value">The value of the index parameter.</param>
		/// <param name="name">The name of the index parameter.</param>
		/// <param name="array">The array the index is intended for.</param>
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		public static void Index<T>(int value, string name, T[] array)
		{
			Null(name, nameof(name));

			IndexInternal(value, name, array.Length);
		}

		/// <summary>
		/// 	Throws an exception if the index is out of bounds of the list.
		/// </summary>
		/// <param name="value">The value of the index parameter.</param>
		/// <param name="name">The name of the index parameter.</param>
		/// <param name="list">The list the index is intended for.</param>
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		public static void Index<T>(int value, string name, IList<T> list)
		{
			Null(name, nameof(name));

			IndexInternal(value, name, list.Count);
		}

#if !NET35
		/// <summary>
		/// 	Throws an exception if the index is out of bounds of the read-only list.
		/// </summary>
		/// <param name="value">The value of the index parameter.</param>
		/// <param name="name">The name of the index parameter.</param>
		/// <param name="list">The array the index is intended for.</param>
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		public static void Index<T>(int value, string name, IReadOnlyList<T> list)
		{
			Null(name, nameof(name));

			IndexInternal(value, name, list.Count);
		}
#endif

		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
		[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
		private static void IndexInternal(int value, string name, int count)
		{
			if (value < 0 || count <= value)
			{
				throw new ArgumentOutOfRangeException(name, value, $"Value was not within the bounds (0 and {count})");
			}
		}

		private static IEnumerable<T> NullElementInternal<T>(IEnumerable<T> value, string name)
		{
			foreach (T element in value)
			{
				if (element is null)
				{
					throw new ArgumentElementNullException(name);
				}

				yield return element;
			}
		}

		private static void NullIndexInternal<T, TList>(TList value, string name, Func<TList, int> countGetter, Func<TList, int, T> indexer)
		{
			var count = countGetter(value);
			for (var i = 0; i < count; ++i)
			{
				var item = indexer(value, i);
				if (item is null)
				{
					throw new ArgumentIndexNullException(name, i);
				}
			}
		}
	}
}
