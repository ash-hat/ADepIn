using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class FunctionServiceBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			new FunctionServiceBinding<bool, Unit>((_0, _1) => true);
			new FunctionServiceBinding<bool, Unit>((IServiceResolver _) => true);
			new FunctionServiceBinding<bool, Unit>((Unit _) => true);
			new FunctionServiceBinding<bool, Unit>(() => true);
		}

		[Fact]
		public void GetWhole()
		{
			var mockFunc = new Mock<WholeBindingImpl<object, object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var resolver = mockResolver.Object;
			var context = new object();
			var ret = Option.Some(new object());
			mockFunc.Setup(x => x.Invoke(resolver, context))
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var binding = new FunctionServiceBinding<object, object>(func);

			var actualRet = binding.Get(resolver, context);

			Assert.Equal(ret, actualRet);
			mockFunc.Verify();
		}

		[Fact]
		public void GetRecursive()
		{
			var mockFunc = new Mock<RecursiveBindingImpl<object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var resolver = mockResolver.Object;
			var ret = Option.Some(new object());
			mockFunc.Setup(x => x.Invoke(resolver))
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var binding = new FunctionServiceBinding<object, Unit>(func);

			var actualRet = binding.Get(resolver, default);

			Assert.Equal(ret, actualRet);
			mockFunc.Verify();
		}

		[Fact]
		public void GetContextual()
		{
			var mockFunc = new Mock<ContextualBindingImpl<object, object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var ret = Option.Some(new object());
			var context = new object();
			mockFunc.Setup(x => x.Invoke(context))
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var resolver = mockResolver.Object;
			var binding = new FunctionServiceBinding<object, object>(func);

			var actualRet = binding.Get(resolver, context);

			Assert.Equal(ret, actualRet);
			mockFunc.Verify();
		}

		[Fact]
		public void GetPure()
		{
			var mockFunc = new Mock<PureBindingImpl<object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var ret = Option.Some(new object());
			mockFunc.Setup(x => x.Invoke())
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var resolver = mockResolver.Object;
			var binding = new FunctionServiceBinding<object, Unit>(func);

			var actualRet = binding.Get(resolver, default);

			Assert.Equal(ret, actualRet);
			mockFunc.Verify();
		}

		[Fact]
		public void GetNopWhole()
		{
			var mockFunc = new Mock<WholeNopBindingImpl<object, object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var resolver = mockResolver.Object;
			var context = new object();
			var ret = new object();
			mockFunc.Setup(x => x.Invoke(resolver, context))
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var binding = new FunctionServiceBinding<object, object>(func);

			var actualRet = binding.Get(resolver, context);

			Assert.Equal(Option.Some(ret), actualRet);
			mockFunc.Verify();
		}

		[Fact]
		public void GetNopRecursive()
		{
			var mockFunc = new Mock<RecursiveNopBindingImpl<object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var resolver = mockResolver.Object;
			var ret = new object();
			mockFunc.Setup(x => x.Invoke(resolver))
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var binding = new FunctionServiceBinding<object, Unit>(func);

			var actualRet = binding.Get(resolver, default);

			Assert.Equal(Option.Some(ret), actualRet);
			mockFunc.Verify();
		}

		[Fact]
		public void GetNopContextual()
		{
			var mockFunc = new Mock<ContextualNopBindingImpl<object, object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var ret = new object();
			var context = new object();
			mockFunc.Setup(x => x.Invoke(context))
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var resolver = mockResolver.Object;
			var binding = new FunctionServiceBinding<object, object>(func);

			var actualRet = binding.Get(resolver, context);

			Assert.Equal(Option.Some(ret), actualRet);
			mockFunc.Verify();
		}

		[Fact]
		public void GetNopPure()
		{
			var mockFunc = new Mock<PureNopBindingImpl<object>>();
			var mockResolver = new Mock<IServiceResolver>();

			var ret = new object();
			mockFunc.Setup(x => x.Invoke())
				.Returns(ret)
				.Verifiable();

			var func = mockFunc.Object;
			var resolver = mockResolver.Object;
			var binding = new FunctionServiceBinding<object, Unit>(func);

			var actualRet = binding.Get(resolver, default);

			Assert.Equal(Option.Some(ret), actualRet);
			mockFunc.Verify();
		}
	}
}
