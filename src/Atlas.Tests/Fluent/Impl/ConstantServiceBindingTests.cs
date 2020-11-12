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
			new ConstantServiceBinding<bool, Unit>(true);
		}

		[Fact]
		public void Get()
		{
			var value = new object();
			var binding = new ConstantServiceBinding<object, Unit>(value);
			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;

			var ret = binding.Get(resolver, default);

			Assert.Equal(value, ret);
		}
	}
}
