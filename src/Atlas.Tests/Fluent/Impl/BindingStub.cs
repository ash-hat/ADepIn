using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Atlas.Fluent.Impl.Tests
{
	public class BindingStubTests
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Ctor()
		{
			new BindingStub<bool, Unit>(x => { });
		}

		[Fact]
		public void Properties()
		{
			StubApplicator<bool, Unit> applicator = x => { };
			var pending = new BindingStub<bool, Unit>(applicator);

			Assert.Equal(applicator, pending.Applicator);
		}
	}
}
