using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class SingletonNopServiceBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			var mockBinding = new Mock<IServiceBinding<bool, Unit>>();
			var binding = mockBinding.Object;

			new SingletonNopServiceBinding<bool, Unit>(binding);
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void Get(bool value)
		{
			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;
			var result = value;
			var binding = new SingletonNopServiceBinding<bool, Unit>(new FunctionServiceBinding<bool, Unit>(() =>
			{
				var copy = result;
				result = !result;
				return copy;
			}));

			var ret1 = binding.Get(resolver, default);
			var ret2 = binding.Get(resolver, default);

			Assert.Equal(Option.Some(value), ret1);
			Assert.Equal(Option.Some(value), ret2);
			// Invert value because the function (which inverts result) is called once)
			Assert.Equal(!value, result);
		}
	}
}
