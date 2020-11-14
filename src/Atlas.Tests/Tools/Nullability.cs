using Xunit;

namespace Atlas.Tests
{
	public class NullabilityTests
	{
		[Fact]
		public void IsNullable_NonNull()
		{
			var isNullable = Nullability<int>.IsNullable;

			Assert.False(isNullable);
		}

		[Fact]
		public void IsNullable_Null()
		{
			var isNullable = Nullability<object>.IsNullable;

			Assert.True(isNullable);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		public void IsNull_NonNull(int value)
		{
			var isNull = Nullability<int>.IsNull(value);

			Assert.False(isNull);
		}

		[Fact]
		public void IsNull_Null()
		{
			var isNullNull = Nullability<object>.IsNull(null);
			var isNullNonNull = Nullability<object>.IsNull(new object());

			Assert.True(isNullNull);
			Assert.False(isNullNonNull);
		}
	}
}
