using Moq;
using Xunit;

namespace Atlas.Fluent.Tests
{
	public class PendingBindingExtensionsTests
	{
		[Fact]
		public void ToConstant()
		{
			var mock = new Mock<IPendingBinding<object>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull);

			var pendingBinding = mock.Object;

			pendingBinding.ToConstant(new object());
		}

		[Fact]
		public void ToMethod()
		{
			var mock = new Mock<IPendingBinding<object>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull);

			var pendingBinding = mock.Object;

			Assert.NotNull(pendingBinding.ToMethod(() => new object()));
		}

		[Fact]
		public void ToMethod_Factory()
		{
			var mock = new Mock<IPendingBinding<object>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull);

			var pendingBinding = mock.Object;

			Assert.NotNull(pendingBinding.ToMethod(x => new object()));
		}
	}
}
