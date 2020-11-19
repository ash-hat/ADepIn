using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace ADepIn.Impl.Tests
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
		public void Bind()
		{
			var mockBinding = new Mock<IServiceBinding<Unit, Unit>>();
			var binding = mockBinding.Object;

			var kernel = new StandardServiceKernel();

			kernel.Bind(binding);
		}

		[Fact]
		public void Get()
		{
			var value = Option.Some(new object());
			var context = new object();
			var kernel = new StandardServiceKernel();
			var mockBinding = new Mock<IServiceBinding<object, object>>();
			mockBinding.Setup(services => services.Get(kernel, context))
				.Returns(value);
			var binding = mockBinding.Object;

			kernel.Bind(binding);
			var resolved = kernel.Get<object, object>(context);

			Assert.Equal(value, resolved);
		}

		[Fact]
		public void Get_NoBinding()
		{
			var kernel = new StandardServiceKernel();

			Assert.Equal(Option.None<Unit>(), kernel.Get<Unit>());
		}

		[Fact]
		public void Get_RecursiveOverflow()
		{
			var kernel = new StandardServiceKernel
			{
				MaxRecursion = Option.Some(0)
			};
			var mockBinding = new Mock<IServiceBinding<Unit, Unit>>();
			mockBinding.Setup(x => x.Get(kernel, default))
				.Returns((IServiceResolver services, Unit _) => services.Get<Unit, Unit>(default));
			var binding = mockBinding.Object;

			kernel.Bind(binding);

			Assert.Throws<InvalidOperationException>(() => kernel.Get<Unit>());
		}

		[Fact]
		public void Get_Contextual()
		{
			var context = new object();
			var value = Option.Some(new object());
			var kernel = new StandardServiceKernel();
			var mockBinding = new Mock<IServiceBinding<object, object>>();
			mockBinding.Setup(x => x.Get(kernel, context))
				.Returns(value);
			var binding = mockBinding.Object;

			kernel.Bind(binding);
			var resolved = kernel.Get<object, object>(context);

			Assert.Equal(value, resolved);
		}
	}
}
