using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD1_0
using System.Reflection;
#endif

namespace Atlas
{
	/// <summary>
	/// 	Contains information about the nullability of this type and the comparison of and instance of the type to <see langword="null"/>.
	/// </summary>
	/// <typeparam name="T">The type to store information about.</typeparam>
	public static class Nullability<T>
	{
		/// <summary>
		/// 	Checks if a value is <see langword="null"/>
		/// </summary>
		/// <param name="value">The possibly-null value to check.</param>
		public delegate bool NullCheck([AllowNull] T value);

		/// <summary>
		/// 	Whether or not this type is nullable.
		/// </summary>
		// ReSharper disable once StaticMemberInGenericType
		public static bool IsNullable { get; }
		/// <summary>
		/// 	The <seealso cref="NullCheck"/> to be used for this type.
		/// </summary>
		public static NullCheck IsNull { get; }

		static Nullability()
		{
			IsNullable = !
#if !NETSTANDARD1_0
				typeof(T)
#else
				typeof(T).GetTypeInfo()
#endif
				.IsValueType;
			IsNull = IsNullable ? (NullCheck) NullableIsNull : NonNullableIsNull;
		}

		private static bool NullableIsNull([AllowNull] T value)
		{
			return value is null;
		}

		private static bool NonNullableIsNull([AllowNull] T value)
		{
			return false;
		}
	}
}
