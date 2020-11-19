using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ADepIn.Tests
{
	public class ArgumentElementNullExceptionTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("non-empty")]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor_ParamName(string? paramName)
		{
			new ArgumentElementNullException(paramName);
		}

		[Theory]
		[InlineData(null, null)]
		[InlineData("non-empty", null)]
		[InlineData(null, "non-empty")]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor_ParamNameAndMessage(string? paramName, string? message)
		{
			new ArgumentElementNullException(paramName, message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("non-empty")]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor_ParamNameAndException(string? paramName)
		{
			new ArgumentElementNullException(paramName, (Exception?) null);
			new ArgumentElementNullException(paramName, new Exception());
		}
	}
}
