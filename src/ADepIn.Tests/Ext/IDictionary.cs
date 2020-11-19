using System;
using System.Collections.Generic;
using Xunit;

namespace ADepIn.Impl.Tests
{
	public class DictionaryExtensionsTests
	{
		private void GetOrInsertInternal(Func<Dictionary<object, object>, object, object, object> getOrInsertVariant)
		{
			var getKey = new object();
			var getValue = new object();
			var insertKey = new object();
			var insertValue = new object();
			var dict = new Dictionary<object, object>
			{
				[getKey] = getValue
			};

			var gotValue = getOrInsertVariant(dict, getKey, new object());

			Assert.Equal(getValue, gotValue);
			Assert.Equal(new Dictionary<object, object>
			{
				[getKey] = getValue
			}, dict);

			var insertedValue = getOrInsertVariant(dict, insertKey, insertValue);

			Assert.Equal(insertValue, insertedValue);
			Assert.Equal(new Dictionary<object, object>
			{
				[getKey] = getValue,
				[insertKey] = insertedValue
			}, dict);
		}

		[Fact]
		public void GetOrInsert()
		{
			GetOrInsertInternal((dict, key, value) => dict.GetOrInsert(key, value));
		}

		[Fact]
		public void GetOrInsertWith()
		{
			GetOrInsertInternal((dict, key, value) => dict.GetOrInsertWith(key, () => value));
		}
	}
}
