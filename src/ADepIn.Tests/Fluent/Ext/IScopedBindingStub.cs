using System;
using ADepIn.Fluent.Impl;
using Moq;
using Xunit;

namespace ADepIn.Fluent.Tests
{
	public class ExtIScopedBindingStubTests
	{
		[Fact]
		public void InSingletonNopScope()
		{
			var mockBinding = new Mock<IServiceBinding<object, Unit>>();
			var mockPendingScopedBinding = new Mock<IScopedBindingStub<object, Unit>>();

			mockBinding.Setup(x => x.Get(It.IsAny<IServiceResolver>(), It.IsAny<Unit>()))
				.Throws<NotSupportedException>();

			var binding = mockBinding.Object;
			mockPendingScopedBinding.Setup(x => x.Applicator)
				.Returns(x => Assert.IsType<SingletonNopServiceBinding<object, Unit>>(x))
				.Verifiable();
			mockPendingScopedBinding.Setup(x => x.Binding)
				.Returns(binding)
				.Verifiable();

			var pendingScopedBinding = mockPendingScopedBinding.Object;

			pendingScopedBinding.InSingletonNopScope();

			mockPendingScopedBinding.Verify();
		}

		[Fact]
		public void InSingletonScope()
		{
			var mockBinding = new Mock<IServiceBinding<object, Unit>>();
			var mockPendingScopedBinding = new Mock<IScopedBindingStub<object, Unit>>();

			mockBinding.Setup(x => x.Get(It.IsAny<IServiceResolver>(), It.IsAny<Unit>()))
				.Throws<NotSupportedException>();

			var binding = mockBinding.Object;
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
			var mockPendingScopedBinding = new Mock<IScopedBindingStub<object, Unit>>();

			mockBinding.Setup(x => x.Get(It.IsAny<IServiceResolver>(), It.IsAny<Unit>()))
				.Throws<NotSupportedException>();

			var binding = mockBinding.Object;
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
