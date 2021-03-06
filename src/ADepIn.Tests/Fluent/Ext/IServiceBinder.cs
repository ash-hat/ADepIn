﻿using Moq;
using Xunit;

namespace ADepIn.Fluent.Tests
{
	public class ExtIServiceBinderTests
	{
		[Fact]
		public void Bind()
		{
			var mockBinder = new Mock<IServiceBinder>();
			var binder = mockBinder.Object;

			var pending = binder.Bind<Unit, Unit>();

			Assert.NotNull(pending);
		}

		[Fact]
		public void Bind_NoContext()
		{
			var mockBinder = new Mock<IServiceBinder>();
			var binder = mockBinder.Object;

			var pending = binder.Bind<Unit>();

			Assert.NotNull(pending);
		}
	}
}
