using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class ConstantServiceBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			new ConstantServiceBinding<bool>(true);
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void Get(bool value)
		{
			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;
			var binding = new ConstantServiceBinding<bool>(value);

			var ret = binding.Get(resolver);

			Assert.Equal(value, ret);
		}
	}
}
