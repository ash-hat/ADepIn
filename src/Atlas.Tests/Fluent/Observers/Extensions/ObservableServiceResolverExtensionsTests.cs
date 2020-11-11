using System;
using Moq;
using Xunit;

namespace Atlas.Fluent.Tests
{
	public class ObservableServiceResolverExtensionsTests
	{
		[Fact]
		public void Observe()
		{
			var mockObservable = new Mock<IObservableServiceResolver>();
			mockObservable.Setup(x => x.Observe<bool>(It.IsAny<IServiceObserver<bool>>()))
				.Throws<NotSupportedException>();
			var observable = mockObservable.Object;

			var pending = observable.Observe<bool>();

			Assert.NotNull(pending);
		}
	}
}
