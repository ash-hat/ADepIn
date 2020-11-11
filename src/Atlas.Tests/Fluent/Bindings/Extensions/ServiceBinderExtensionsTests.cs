using Moq;
using Xunit;

namespace Atlas.Fluent.Tests
{
	public class ServiceBinderExtensionsTests
	{
		[Fact]
		public void Bind()
		{
			var mock = new Mock<IServiceBinder>();

			var binder = mock.Object;

			Assert.NotNull(binder.Bind<object>());
		}
	}
}
