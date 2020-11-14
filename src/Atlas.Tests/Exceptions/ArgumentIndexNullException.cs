using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Atlas.Tests
{
	public class ArgumentIndexNullExceptionTests
	{
		[Theory]
		[InlineData(null, 0)]
		[InlineData("non-empty", 1)]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor_ParamNameAndIndex(string? paramName, int index)
		{
			new ArgumentIndexNullException(paramName, index);
		}

		[Theory]
		[InlineData(null, null, 0)]
		[InlineData("non-empty", null, 1)]
		[InlineData(null, "non-empty", 2)]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor_ParamNameAndMessageAndIndex(string? paramName, string? message, int index)
		{
			new ArgumentIndexNullException(paramName, message, index);
		}

		[Theory]
		[InlineData(null, 0)]
		[InlineData("non-empty", 1)]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor_ParamNameAndExceptionAndIndex(string? paramName, int index)
		{
			new ArgumentIndexNullException(paramName, (Exception?) null, index);
			new ArgumentIndexNullException(paramName, new Exception(), index);
		}

		[Fact]
		public void Ctor_ParamNameAndIndex_Negative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new ArgumentIndexNullException(null, -1));
		}

		[Fact]
		public void Ctor_ParamNameAndMessageAndIndex_Negative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new ArgumentIndexNullException(null, (string?) null, -1));
		}

		[Fact]
		public void Ctor_ParamNameAndExceptionAndIndex_Negative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new ArgumentIndexNullException(null, (Exception?) null, -1));
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(int.MaxValue)]
		public void Index_Getter(int index)
		{
			var e = new ArgumentIndexNullException(null, index);

			Assert.Equal(index, e.Index);
		}

		[Fact]
		public void Message_Getter()
		{
			var e = new ArgumentIndexNullException(null, 0);

			Assert.NotNull(e.Message);
		}
	}
}
