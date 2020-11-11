using System;
using System.Diagnostics.CodeAnalysis;
using Atlas.Fluent;
using Moq;
using Xunit;

namespace Atlas.Impl.Tests
{
	public class StandardServiceKernelTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			new StandardServiceKernel();
		}

		[Fact]
		public void MaxRecursion_Getter()
		{
			var kernel = new StandardServiceKernel();

			var max = kernel.MaxRecursion.Unwrap();

			Assert.InRange(max, 0, int.MaxValue);
		}

		[Fact]
		public void MaxRecursion_Setter_None()
		{
			var kernel = new StandardServiceKernel
			{
				MaxRecursion = Option.None<int>()
			};

			Assert.Equal(Option.None<int>(), kernel.MaxRecursion);
		}

		[Theory]
		[InlineData(10)]
		[InlineData(5)]
		[InlineData(0)]
		public void MaxRecursion_Setter_Identical(int max)
		{
			var opt = Option.Some(max);
			var kernel = new StandardServiceKernel
			{
				MaxRecursion = opt
			};

			kernel.MaxRecursion = opt;

			Assert.Equal(opt, kernel.MaxRecursion);
		}

		[Theory]
		[InlineData(10)]
		[InlineData(5)]
		[InlineData(0)]
		public void MaxRecursion_Setter_SomePositive(int max)
		{
			var opt = Option.Some(max);
			var kernel = new StandardServiceKernel
			{
				MaxRecursion = opt
			};

			Assert.Equal(opt, kernel.MaxRecursion);
		}

		[Fact]
		public void MaxRecursion_Setter_SomeNegative()
		{
			var kernel = new StandardServiceKernel();

			Assert.Throws<ArgumentOutOfRangeException>(() => kernel.MaxRecursion = Option.Some(-1));
		}

		[Fact]
		public void Bind()
		{
			var mock = new Mock<IServiceBinding<int>>();

			var kernel = new StandardServiceKernel();
			var binding = mock.Object;

			kernel.Bind(binding);
		}

		[Fact]
		public void Get()
		{
			var value = Guid.NewGuid();
			var mock = new Mock<IServiceBinding<Guid>>();
			mock.Setup(x => x.Get(It.IsAny<IServiceResolver>()))
				.Returns(value);

			var kernel = new StandardServiceKernel();
			var binding = mock.Object;
			kernel.Bind(binding);

			Assert.Equal(value, kernel.Get<Guid>().Unwrap());
		}

		[Fact]
		public void Get_NoBinding()
		{
			var kernel = new StandardServiceKernel();

			Assert.Equal(Option.None<int>(), kernel.Get<int>());
		}

		[Fact]
		public void Get_RecursiveOverflow()
		{
			var mock = new Mock<IServiceBinding<int>>();
			mock.Setup(x => x.Get(It.IsAny<IServiceResolver>()))
				.Returns((IServiceResolver services) => services.Get<int>().Unwrap());

			var kernel = new StandardServiceKernel
			{
				MaxRecursion = Option.Some(0)
			};
			var binding = mock.Object;

			kernel.Bind(binding);

			Assert.Throws<InvalidOperationException>(() => kernel.Get<int>());
		}

		[Fact]
		public void Observe()
		{
			var mock = new Mock<IServiceObserver<bool>>();
			var called = false;
			mock.Setup(x => x.Notify(It.IsAny<IObservableServiceResolver>(), It.IsAny<Func<bool>>()))
				.Callback(() => called = true);

			var kernel = new StandardServiceKernel();
			var observer = mock.Object;
			kernel.Observe(observer);
			Assert.False(called);

			kernel.Bind<bool>().ToConstant(false);

			Assert.True(called);
		}

		[Fact]
		public void Observe_AlreadyBound()
		{
			var mock = new Mock<IServiceObserver<bool>>();
			var called = false;
			mock.Setup(x => x.Notify(It.IsAny<IObservableServiceResolver>(), It.IsAny<Func<bool>>()))
				.Callback(() => called = true);

			var kernel = new StandardServiceKernel();
			var observer = mock.Object;
			kernel.Bind<bool>().ToConstant(true);

			kernel.Observe(observer);

			Assert.True(called);
		}
	}
}
