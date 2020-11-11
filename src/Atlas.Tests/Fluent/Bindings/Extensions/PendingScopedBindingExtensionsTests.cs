using Moq;
using Xunit;

namespace Atlas.Fluent.Tests
{
	public class PendingScopedBindingExtensionsTests
	{
		[Fact]
		public void InSingletonScope()
		{
			var mockBinding = new Mock<IServiceBinding<object>>();
			mockBinding.Setup(x => x.Get(It.IsAny<IServiceResolver>()))
				.Returns(new object());

			var mockPendingScopedBinding = new Mock<IPendingScopedBinding<object>>();
			mockPendingScopedBinding.Setup(x => x.Applicator)
				.Returns(Assert.NotNull);
			mockPendingScopedBinding.Setup(x => x.Binding)
				.Returns(mockBinding.Object);

			var pendingScopedBinding = mockPendingScopedBinding.Object;

			pendingScopedBinding.InSingletonScope();
		}

		[Fact]
		public void InTransientScope()
		{
			var mockBinding = new Mock<IServiceBinding<object>>();
			mockBinding.Setup(x => x.Get(It.IsAny<IServiceResolver>()))
				.Returns(new object());

			var mockPendingScopedBinding = new Mock<IPendingScopedBinding<object>>();
			mockPendingScopedBinding.Setup(x => x.Applicator)
				.Returns(Assert.NotNull);
			mockPendingScopedBinding.Setup(x => x.Binding)
				.Returns(mockBinding.Object);

			var pendingScopedBinding = mockPendingScopedBinding.Object;

			pendingScopedBinding.InTransientScope();
		}
	}
}
