using System;
using System.Runtime.Serialization;

namespace Atlas
{
	/// <summary>
	/// 	The exception that is thrown when one of the elements of an argument provided to the method is <see langword="null" />.
	/// </summary>
	public class ArgumentElementNullException : ArgumentException
	{
		/// <summary>
		/// 	Creates an instance of <see cref="ArgumentElementNullException" />.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		public ArgumentElementNullException(string? paramName)
			: base("Element cannot be null.", paramName)
		{
		}

		/// <summary>
		/// 	Creates an instance of <see cref="ArgumentElementNullException" />.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public ArgumentElementNullException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// 	Creates an instance of <see cref="ArgumentElementNullException" />.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public ArgumentElementNullException(string? paramName, string? message)
			: base(message, paramName)
		{
		}
	}
}
