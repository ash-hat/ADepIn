using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class SingletonServiceBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			var mockBinding = new Mock<IServiceBinding<bool, Unit>>();
			var binding = mockBinding.Object;

			new SingletonServiceBinding<bool, Unit>(binding);
		}

		[Fact]
		public void Get()
		{
			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;
			var binding = new SingletonServiceBinding<object, object>(new FunctionServiceBinding<object, object>(() => new object()));
			
			var in1 = new object();
			var in2 = new object();

			var ret1 = binding.Get(resolver, in1);
			var ret2 = binding.Get(resolver, in2);
			var ret3 = binding.Get(resolver, in1);
			var ret4 = binding.Get(resolver, in2);

			Assert.NotEqual(ret1, ret2);
			Assert.Equal(ret1, ret3);
			Assert.Equal(ret2, ret4);
		}
	}
}
