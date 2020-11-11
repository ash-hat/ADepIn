using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class GetServiceObserverTests
	{
	 	[Fact]
	 	[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
	 	public void Ctor()
	 	{
	 	 	new GetServiceObserver<bool>(x => { });
	 	}

	 	[Fact]
	 	public void Notify()
	 	{
	 	 	var service = true;
	 	 	var mockServices = new Mock<IObservableServiceResolver>();
	 	 	var services = mockServices.Object;
	 	 	var observed = false;
	 	 	var observer = new GetServiceObserver<bool>(x =>
	 	 	{
	 	 	 	Assert.Equal(service, x);
	 	 	 	observed = true;
	 	 	});

	 	 	observer.Notify(null, () => service);

	 	 	Assert.True(observed);
	 	}
	}
}
