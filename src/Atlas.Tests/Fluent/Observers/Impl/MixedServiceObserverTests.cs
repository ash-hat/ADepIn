using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class MixedServiceObserverTests
	{
	 	[Fact]
	 	[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
	 	public void Ctor()
	 	{
	 	 	new MixedServiceObserver<bool>((x, y) => { });
	 	}

	 	[Fact]
	 	public void Notify()
	 	{
	 	 	var mockServices = new Mock<IObservableServiceResolver>();
	 	 	var services = mockServices.Object;
	 	 	var service = true;
	 	 	var observed = false;
	 	 	var observer = new MixedServiceObserver<bool>((x, y) =>
	 	 	{
	 	 	 	Assert.Equal(services, x);
	 	 	 	Assert.Equal(service, y);
	 	 	 	observed = true;
	 	 	});

	 	 	observer.Notify(services, () => service);

	 	 	Assert.True(observed);
	 	}
	}
}
