using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class PendingScopedBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			var mockBinding = new Mock<IServiceBinding<bool>>();
			var binding = mockBinding.Object;

			new PendingScopedBinding<bool>(x => { }, binding);
		}

		[Fact]
		public void Properties()
		{
			var mockBinding = new Mock<IServiceBinding<bool>>();
			Action<IServiceBinding<bool>> applicator = x => { };
			var binding = mockBinding.Object;

			var pending = new PendingScopedBinding<bool>(applicator, binding);

			Assert.Equal(applicator, pending.Applicator);
			Assert.Equal(binding, pending.Binding);
		}
	}
}
