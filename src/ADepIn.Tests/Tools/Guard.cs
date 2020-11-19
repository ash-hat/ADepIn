using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ADepIn.Tests
{
	public class GuardTests
	{
		[Fact]
		public void Null()
		{
			var obj = new object();
			const string name = nameof(obj);

			Guard.Null(obj, name);
		}

		[Fact]
		public void Null_NullObj()
		{
			object? obj = null;
			const string name = nameof(obj);

			Assert.Throws<ArgumentNullException>(() => Guard.Null(obj, name));
		}

		[Fact]
		public void Null_NullName()
		{
			var obj = new object();
			const string? name = null;

			Assert.Throws<ArgumentNullException>(() => Guard.Null(obj, name!));
		}

		[Theory]
		[InlineData("a")]
		[InlineData(" a")]
		[InlineData("a ")]
		[InlineData(" a ")]
		public void NullOrWhiteSpace(string str)
		{
			const string name = nameof(str);

			Guard.NullOrWhiteSpace(str, name);
		}

		[Fact]
		public void NullOrWhiteSpace_Null()
		{
			const string? str = null;
			const string name = nameof(str);

			Assert.Throws<ArgumentNullException>(() => Guard.NullOrWhiteSpace(str, name));
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("  ")]
		public void NullOrWhiteSpace_WhiteSpace(string str)
		{
			const string name = nameof(str);

			Assert.Throws<ArgumentException>(() => Guard.NullOrWhiteSpace(str, name));
		}

		[Fact]
		public void NullElement()
		{
			var obj = new object();
			var value = new[]
			{
				obj
			};
			const string name = nameof(value);

			IEnumerable<object> result = Guard.NullElement(value, name);

			Assert.Equal(value, result);
		}

		[Fact]
		public void NullElement_NullElement()
		{
			object obj = null!;
			var value = new[]
			{
				obj
			};
			const string name = nameof(value);

			IEnumerable<object> result = Guard.NullElement(value, name);

			Assert.Throws<ArgumentElementNullException>(() => result.ToList());
		}

		[Fact]
		public void NullIndex()
		{
			var obj = new object();
			var value = new[]
			{
				obj
			};
			const string name = nameof(value);

			Guard.NullIndex(value, name);
			Guard.NullIndex((IList<object?>) value, name);
			Guard.NullIndex((IReadOnlyList<object?>) value, name);
		}

		[Fact]
		public void NullIndex_NonNull()
		{
			var obj = new int();
			var value = new[]
			{
				obj
			};
			const string name = nameof(value);

			Guard.NullIndex(value, name);
			Guard.NullIndex((IList<int>) value, name);
			Guard.NullIndex((IReadOnlyList<int>) value, name);
		}

		[Fact]
		public void NullIndex_NullIndex()
		{
			object obj = null!;
			var value = new[]
			{
				obj
			};
			const string name = nameof(value);

			Assert.Throws<ArgumentIndexNullException>(() => Guard.NullIndex(value, name));
			Assert.Throws<ArgumentIndexNullException>(() => Guard.NullIndex((IList<object?>) value, name));
			Assert.Throws<ArgumentIndexNullException>(() => Guard.NullIndex((IReadOnlyList<object?>) value, name));
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void Negative(int value)
		{
			const string name = nameof(value);

			Guard.Negative(value, name);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(-2)]
		[InlineData(-3)]
		public void Negative_Negative(int value)
		{
			const string name = nameof(value);

			Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Negative(value, name));
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void NonPositive(int value)
		{
			const string name = nameof(value);

			Guard.NonPositive(value, name);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-2)]
		public void NonPositive_NonPositive(int value)
		{
			const string name = nameof(value);

			Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NonPositive(value, name));
		}

		[Theory]
		[InlineData(0, 1)]
		[InlineData(0, 2)]
		[InlineData(1, 2)]
		[InlineData(0, 3)]
		[InlineData(1, 3)]
		[InlineData(2, 3)]
		public void Index(int index, int count)
		{
			const string name = nameof(index);
			var array = new int[count];

			Guard.Index(index, name, array);
			Guard.Index(index, name, (IList<int>) array);
			Guard.Index(index, name, (IReadOnlyList<int>) array);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 0)]
		[InlineData(-1, 0)]
		[InlineData(1, 1)]
		public void Index_BadIndex(int index, int count)
		{
			const string name = nameof(index);
			var array = new int[count];

			Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Index(index, name, array));
			Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Index(index, name, (IList<int>) array));
			Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Index(index, name, (IReadOnlyList<int>) array));
		}
	}
}
