using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace ADepIn.Fluent.Impl.Tests
{
	public class ScopedBindingStubTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			var mockBinding = new Mock<IServiceBinding<bool, Unit>>();
			var binding = mockBinding.Object;

			new ScopedBindingStub<bool, Unit>(x => { }, binding);
		}

		[Fact]
		public void Properties()
		{
			StubApplicator<bool, Unit> applicator = x => { };
			var mockBinding = new Mock<IServiceBinding<bool, Unit>>();
			var binding = mockBinding.Object;

			var pending = new ScopedBindingStub<bool, Unit>(applicator, binding);

			Assert.Equal(applicator, pending.Applicator);
			Assert.Equal(binding, pending.Binding);
		}
	}
}
