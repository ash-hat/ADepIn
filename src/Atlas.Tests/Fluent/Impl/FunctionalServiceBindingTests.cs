using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class FunctionalServiceBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			new FunctionalServiceBinding<bool, Unit>((_0, _1) => true);
			new FunctionalServiceBinding<bool, Unit>((IServiceResolver _) => true);
			new FunctionalServiceBinding<bool, Unit>((Unit _) => true);
			new FunctionalServiceBinding<bool, Unit>(() => true);
		}

		[Fact]
		public void GetWhole()
		{
			var bindingResolver = Option.None<IServiceResolver>();
			var bindingContext = Option.None<object>();
			var ret = new object();
			var binding = new FunctionalServiceBinding<object, object>((services, context) =>
			{
				bindingResolver.Replace(services);
				bindingContext.Replace(context);
				return ret;
			});

			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;
			var context = new object();

			var actualRet = binding.Get(resolver, context);

			Assert.Equal(ret, actualRet);
			Assert.Equal(Option.Some(resolver), bindingResolver);
			Assert.Equal(Option.Some(context), bindingContext);
		}

		[Fact]
		public void GetRecursive()
		{
			var bindingResolver = Option.None<IServiceResolver>();
			var ret = new object();
			var binding = new FunctionalServiceBinding<object, Unit>((IServiceResolver resolver) =>
			{
				bindingResolver.Replace(resolver);
				return ret;
			});

			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;

			var actualRet = binding.Get(resolver, default);

			Assert.Equal(ret, actualRet);
			Assert.Equal(Option.Some(resolver), bindingResolver);
		}

		[Fact]
		public void GetContextual()
		{
			var bindingContext = Option.None<object>();
			var ret = new object();
			var binding = new FunctionalServiceBinding<object, object>((object context) =>
			{
				bindingContext.Replace(context);
				return ret;
			});

			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;
			var context = new object();

			var actualRet = binding.Get(resolver, context);

			Assert.Equal(ret, actualRet);
			Assert.Equal(Option.Some(context), bindingContext);
		}

		[Fact]
		public void GetPure()
		{
			var ret = new object();
			var binding = new FunctionalServiceBinding<object, Unit>(() => ret);

			var mockResolver = new Mock<IServiceResolver>();
			var resolver = mockResolver.Object;

			var actualRet = binding.Get(resolver, default);

			Assert.Equal(ret, actualRet);
		}
	}
}
