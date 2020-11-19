using System;
using Moq;
using Xunit;

namespace ADepIn.Fluent.Tests
{
	public class ExtIBindingStubTests
	{
		[Fact]
		public void ToConstant()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			pendingBinding.ToConstant(new object());

			mock.Verify();
		}

		[Fact]
		public void ToWholeMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToWholeMethod((_0, _1) => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}

		[Fact]
		public void ToRecursiveMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToRecursiveMethod(_ => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}

		[Fact]
		public void ToContextualMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToContextualMethod(_ => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}

		[Fact]
		public void ToPureMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToPureMethod(() => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}

		[Fact]
		public void ToWholeNopMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToWholeNopMethod((_0, _1) => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}

		[Fact]
		public void ToRecursiveNopMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToRecursiveNopMethod(_ => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}

		[Fact]
		public void ToContextualNopMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToContextualNopMethod(_ => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}

		[Fact]
		public void ToPureNopMethod()
		{
			var mock = new Mock<IBindingStub<object, Unit>>();
			mock.Setup(x => x.Applicator)
				.Returns(Assert.NotNull)
				.Verifiable();
			var pendingBinding = mock.Object;

			var pending = pendingBinding.ToPureNopMethod(() => throw new NotSupportedException());

			Assert.NotNull(pending);
			mock.Verify();
		}
	}
}
