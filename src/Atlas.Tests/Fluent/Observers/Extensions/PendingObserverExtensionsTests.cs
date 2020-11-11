using System;
using Atlas.Fluent.Impl;
using Moq;
using Xunit;

namespace Atlas.Fluent.Tests
{
	public class PendingObserverExtensionsTests
	{
		[Fact]
		public void ToMixedMethod()
		{
			IServiceObserver<bool>? observer = null;
			var mockObservable = new Mock<IObservableServiceResolver>();
			mockObservable.Setup(x => x.Observe<bool>(It.IsAny<IServiceObserver<bool>>()))
				.Callback<IServiceObserver<bool>>(x => 
				{
					Assert.NotNull(x);
					observer = x;
				});
			var observable = mockObservable.Object;

			observable.Observe<bool>().ToMixedMethod((x, y) => { });

			Assert.NotNull(observer);
			Assert.Equal(typeof(MixedServiceObserver<bool>), observer!.GetType());
		}

		[Fact]
		public void ToServicesMethod()
		{
			IServiceObserver<bool>? observer = null;
			var mockObservable = new Mock<IObservableServiceResolver>();
			mockObservable.Setup(x => x.Observe<bool>(It.IsAny<IServiceObserver<bool>>()))
				.Callback<IServiceObserver<bool>>(x => 
				{
					Assert.NotNull(x);
					observer = x;
				});
			var observable = mockObservable.Object;

			observable.Observe<bool>().ToServicesMethod(x => { });

			Assert.NotNull(observer);
			Assert.Equal(typeof(ServicesServiceObserver<bool>), observer!.GetType());
		}

		[Fact]
		public void ToGetMethod()
		{
			IServiceObserver<bool>? observer = null;
			var mockObservable = new Mock<IObservableServiceResolver>();
			mockObservable.Setup(x => x.Observe<bool>(It.IsAny<IServiceObserver<bool>>()))
				.Callback<IServiceObserver<bool>>(x => 
				{
					Assert.NotNull(x);
					observer = x;
				});
			var observable = mockObservable.Object;

			observable.Observe<bool>().ToGetMethod(x => { });

			Assert.NotNull(observer);
			Assert.Equal(typeof(GetServiceObserver<bool>), observer!.GetType());
		}
	}
}
