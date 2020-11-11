using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class ServicesServiceObserverTests
	{
	 	[Fact]
	 	[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
	 	public void Ctor()
	 	{
	 	 	new ServicesServiceObserver<bool>(x => { });
	 	}

	 	[Fact]
	 	public void Notify()
	 	{
	 	 	var mockServices = new Mock<IObservableServiceResolver>();
	 	 	var services = mockServices.Object;
	 	 	var observed = false;
	 	 	var observer = new ServicesServiceObserver<bool>(x =>
	 	 	{
	 	 	 	Assert.Equal(services, x);
	 	 	 	observed = true;
	 	 	});

	 	 	observer.Notify(services, null);

	 	 	Assert.True(observed);
	 	}
	}
}
