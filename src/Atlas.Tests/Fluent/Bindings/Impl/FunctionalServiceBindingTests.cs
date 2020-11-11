using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class FunctionalServiceBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			new FunctionalServiceBinding<bool>(() => true);
		}

		[Fact]
		public void Get()
		{
			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;
			var result = false;
			var binding = new FunctionalServiceBinding<bool>(() =>
			{
				result = true;
				return result;
			});

			var ret = binding.Get(resolver);

			Assert.True(ret);
			Assert.True(result);
		}
	}
}
