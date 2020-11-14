using System;

namespace Atlas
{
	/// <summary>
	/// 	The exception that is thrown when one of the indexable elements of an argument provided to the method is <see langword="null" />.
	/// </summary>
	public class ArgumentIndexNullException : ArgumentElementNullException
	{
		/// <summary>
		/// 	The index of the element within the argument that caused the exception.
		/// </summary>
		public int Index { get; }

		/// <inheritdoc cref="ArgumentException.Message" />
		public override string Message => base.Message + " Index: " + Index + ".";

		/// <summary>
		/// 	Creates an instance of <see cref="ArgumentIndexNullException" />.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <param name="index">The index within the parameter that caused the exception.</param>
		public ArgumentIndexNullException(string? paramName, int index)
			: base(paramName)
		{
			Guard.Negative(index, nameof(index));

			Index = index;
		}

		/// <summary>
		/// 	Creates an instance of <see cref="ArgumentIndexNullException" />.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <param name="index">The index within the parameter that caused the exception.</param>
		public ArgumentIndexNullException(string? message, Exception? innerException, int index)
			: base(message, innerException)
		{
			Guard.Negative(index, nameof(index));

			Index = index;
		}

		/// <summary>
		/// 	Creates an instance of <see cref="ArgumentIndexNullException" />.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="index">The index within the parameter that caused the exception.</param>
		public ArgumentIndexNullException(string? paramName, string? message, int index)
			: base(message, paramName)
		{
			Guard.Negative(index, nameof(index));

			Index = index;
		}
	}
}
