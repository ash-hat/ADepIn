using System;
using Atlas.Fluent.Impl;
using Moq;
using Xunit;

namespace Atlas.Fluent.Tests
{
	public class PendingScopedBindingExtensionsTests
	{
		[Fact]
		public void InSingletonScope()
		{
			var mockBinding = new Mock<IServiceBinding<object, Unit>>();
			mockBinding.Setup(x => x.Get(It.IsAny<IServiceResolver>(), It.IsAny<Unit>()))
				.Throws<NotSupportedException>();
			var binding = mockBinding.Object;

			var mockPendingScopedBinding = new Mock<IPendingScopedBinding<object, Unit>>();
			mockPendingScopedBinding.Setup(x => x.Applicator)
				.Returns(x => Assert.IsType<SingletonServiceBinding<object, Unit>>(x))
				.Verifiable();
			mockPendingScopedBinding.Setup(x => x.Binding)
				.Returns(binding)
				.Verifiable();
			var pendingScopedBinding = mockPendingScopedBinding.Object;

			pendingScopedBinding.InSingletonScope();

			mockPendingScopedBinding.Verify();
		}

		[Fact]
		public void InTransientScope()
		{
			var mockBinding = new Mock<IServiceBinding<object, Unit>>();
			mockBinding.Setup(x => x.Get(It.IsAny<IServiceResolver>(), It.IsAny<Unit>()))
				.Throws<NotSupportedException>();
			var binding = mockBinding.Object;

			var mockPendingScopedBinding = new Mock<IPendingScopedBinding<object, Unit>>();
			mockPendingScopedBinding.Setup(x => x.Applicator)
				.Returns(x => Assert.Equal(binding, x))
				.Verifiable();
			mockPendingScopedBinding.Setup(x => x.Binding)
				.Returns(binding)
				.Verifiable();
			var pendingScopedBinding = mockPendingScopedBinding.Object;

			pendingScopedBinding.InTransientScope();

			mockPendingScopedBinding.Verify();
		}
	}
}
