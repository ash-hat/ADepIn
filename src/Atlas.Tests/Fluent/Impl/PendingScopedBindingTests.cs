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
			var mockBinding = new Mock<IServiceBinding<bool, Unit>>();
			var binding = mockBinding.Object;

			new PendingScopedBinding<bool, Unit>(x => { }, binding);
		}

		[Fact]
		public void Properties()
		{
			PendingApplicator<bool, Unit> applicator = x => { };
			var mockBinding = new Mock<IServiceBinding<bool, Unit>>();
			var binding = mockBinding.Object;

			var pending = new PendingScopedBinding<bool, Unit>(applicator, binding);

			Assert.Equal(applicator, pending.Applicator);
			Assert.Equal(binding, pending.Binding);
		}
	}
}
