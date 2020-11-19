using System;
using System.Linq;
using Moq;
using Xunit;

namespace ADepIn.Tests
{
	public class ServiceKernelExtensionsTests
	{
		[Fact]
		public void LoadEntryType_Generic()
		{
			var mockKernel = new Mock<IServiceKernel>();
			var kernel = mockKernel.Object;

			var module = kernel.LoadEntryType<MockModule>();

			Assert.True(module.Loaded);
		}

		[Fact]
		public void LoadEntryType_Parameterized()
		{
			var mockKernel = new Mock<IServiceKernel>();
			var kernel = mockKernel.Object;

			var module = kernel.LoadEntryType(typeof(MockModule)).Unwrap();

			var typedModule = Assert.IsType<MockModule>(module);
			Assert.True(typedModule.Loaded);
		}

		[Theory]
		[InlineData(typeof(bool))]
		[InlineData(typeof(int))]
		public void LoadEntryType_Parameterized_FailedAssignability(Type entryType)
		{
			var mockKernel = new Mock<IServiceKernel>();
			var kernel = mockKernel.Object;

			Assert.Equal(Option.None<IModule>(), kernel.LoadEntryType(entryType));
		}

		[Theory]
		[InlineData(typeof(string))]
		[InlineData(typeof(string[]))]
		public void LoadEntryType_Parameterized_FailedConstraint(Type entryType)
		{
			var mockKernel = new Mock<IServiceKernel>();
			var kernel = mockKernel.Object;

			Assert.Equal(Option.None<IModule>(), kernel.LoadEntryType(entryType));
		}

		[Fact]
		public void LoadEntryTypes()
		{
			var mockKernel = new Mock<IServiceKernel>();
			var kernel = mockKernel.Object;
			var entryTypes = new[]
			{
				typeof(bool),
				typeof(IModule),
				typeof(MockModule)
			};

			var modules = kernel.LoadEntryTypes(entryTypes).ToList();

			var module = Assert.Single(modules);
			var typedModule = Assert.IsType<MockModule>(module);
			Assert.True(typedModule.Loaded);
		}

		private class MockModule : IEntryModule<MockModule>
		{
			public bool Loaded { get; private set; }

			public MockModule()
			{
				Loaded = false;
			}

			public void Load(IServiceKernel kernel)
			{
				Guard.Null(kernel, nameof(kernel));

				Loaded = true;
			}
		}
	}
}
