using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class PendingBindingTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			new PendingBinding<bool, Unit>(x => { });
		}

		[Fact]
		public void Properties()
		{
			PendingApplicator<bool, Unit> applicator = x => { };
			var pending = new PendingBinding<bool, Unit>(applicator);

			Assert.Equal(applicator, pending.Applicator);
		}
	}
}
