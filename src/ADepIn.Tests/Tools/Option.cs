using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ADepIn.Tests
{
	public class OptionExtensionsTests
	{
		private const bool SOME_INNER = true;
		private const bool NOT_SOME_INNER = !SOME_INNER;
		private static readonly Option<bool> _some = Option.Some(SOME_INNER);
		private static readonly Option<bool> _notSome = Option.Some(NOT_SOME_INNER);
		private static readonly Option<bool> _none = Option.None<bool>();

		[Fact]
		public void _Prerequisites()
		{
			Assert.NotEqual(SOME_INNER, NOT_SOME_INNER);
			Assert.NotEqual(_some, _notSome);
			Assert.NotEqual(_some, _none);
			Assert.NotEqual(_notSome, _none);
		}

		[Fact]
		public void As()
		{
			var str = string.Empty;
			var strAs = str.As<string, object>();

			Assert.True(strAs.MatchSome(out var strCasted));
			Assert.Equal(str, strCasted);

			var obj = new object();
			var objAs = obj.As<object, int>();

			Assert.True(objAs.IsNone);
		}

		[Fact]
		public void Contains_Equatable()
		{
			var someContainsInner = _some.Contains(SOME_INNER);
			var someContainsNotInner = _some.Contains(NOT_SOME_INNER);
			var noneContainsInner = _none.Contains(SOME_INNER);
			var noneContainsNotInner = _none.Contains(NOT_SOME_INNER);

			Assert.True(someContainsInner);
			Assert.False(someContainsNotInner);
			Assert.False(noneContainsInner);
			Assert.False(noneContainsNotInner);
		}

		[Fact]
		public void Contains_Comparer()
		{
			var comparer = EqualityComparer<bool>.Default;

			var someContainsInner = _some.Contains(SOME_INNER, comparer);
			var someContainsNotInner = _some.Contains(NOT_SOME_INNER, comparer);
			var noneContainsInner = _none.Contains(SOME_INNER, comparer);
			var noneContainsNotInner = _none.Contains(NOT_SOME_INNER, comparer);

			Assert.True(someContainsInner);
			Assert.False(someContainsNotInner);
			Assert.False(noneContainsInner);
			Assert.False(noneContainsNotInner);
		}

		[Fact]
		public void Matches_Equatable()
		{
			var someMatchesSome = _some.Matches(_some);
			var someMatchesNotSome = _some.Matches(_notSome);
			var someMatchesNone = _some.Matches(_none);
			var noneMatchesSome = _none.Matches(_some);
			var noneMatchesNotSome = _none.Matches(_notSome);
			var noneMatchesNone = _none.Matches(_none);

			Assert.True(someMatchesSome);
			Assert.False(someMatchesNotSome);
			Assert.False(someMatchesNone);
			Assert.False(noneMatchesSome);
			Assert.False(noneMatchesNotSome);
			Assert.False(noneMatchesNone);
		}

		[Fact]
		public void Matches_Comparer()
		{
			var comparer = EqualityComparer<bool>.Default;

			var someMatchesSome = _some.Matches(_some, comparer);
			var someMatchesNotSome = _some.Matches(_notSome, comparer);
			var someMatchesNone = _some.Matches(_none, comparer);
			var noneMatchesSome = _none.Matches(_some, comparer);
			var noneMatchesNotSome = _none.Matches(_notSome, comparer);
			var noneMatchesNone = _none.Matches(_none, comparer);

			Assert.True(someMatchesSome);
			Assert.False(someMatchesNotSome);
			Assert.False(someMatchesNone);
			Assert.False(noneMatchesSome);
			Assert.False(noneMatchesNotSome);
			Assert.False(noneMatchesNone);
		}

		[Fact]
		public void Flatten()
		{
			var someSome = Option.Some(_some);
			var someNone = Option.Some(_none);
			var none = Option.None<Option<bool>>();

			var someSomeFlattened = someSome.Flatten();
			var someNoneFlattened = someNone.Flatten();
			var noneFlattened = none.Flatten();

			Assert.Equal(_some, someSomeFlattened);
			Assert.Equal(_none, someNoneFlattened);
			Assert.Equal(_none, noneFlattened);
		}

		[Fact]
		public void Take()
		{
			var some = _some;
			var notSome = _notSome;
			var none = _none;

			var someTook = some.Take();
			var notSomeTook = notSome.Take();
			var noneTook = none.Take();

			Assert.Equal(_some, someTook);
			Assert.Equal(_none, some);

			Assert.Equal(_notSome, notSomeTook);
			Assert.Equal(_none, notSome);

			Assert.Equal(none, noneTook);
			Assert.Equal(_none, none);
		}

		[Fact]
		public void GetOrInsert()
		{
			var someMutated = _some;
			var noneMutated = _none;

			var someGot = someMutated.GetOrInsert(NOT_SOME_INNER);
			var noneGot = noneMutated.GetOrInsert(NOT_SOME_INNER);

			Assert.Equal(SOME_INNER, someGot);
			Assert.Equal(NOT_SOME_INNER, noneGot);
			Assert.Equal(_some, someMutated);
			Assert.Equal(_notSome, noneMutated);
		}

		[Fact]
		public void GetOrInsertWith()
		{
			var someMutated = _some;
			var noneMutated = _none;

			var someGot = someMutated.GetOrInsertWith(() => NOT_SOME_INNER);
			var noneGot = noneMutated.GetOrInsertWith(() => NOT_SOME_INNER);

			Assert.Equal(SOME_INNER, someGot);
			Assert.Equal(NOT_SOME_INNER, noneGot);
			Assert.Equal(_some, someMutated);
			Assert.Equal(_notSome, noneMutated);
		}

		[Fact]
		public void WhereSome()
		{
			var items = new[]
			{
				_none,
				_some,
				_none,
				_notSome,
				_none,
				_none,
				_some,
				_notSome
			};

			var itemsWhere = items.Where(x => x.IsSome).Select(x => x.Unwrap());
			var itemsWhereSome = items.WhereSome();

			Assert.Equal(itemsWhere, itemsWhereSome);
		}

		[Fact]
		public void WhereSelect()
		{
			const int halfCount = 5;
			const int increment = 5;
			var items = Enumerable.Range(0, halfCount * 2).ToArray();

			bool Filter(int x)
			{
				return x > halfCount;
			}

			int Mutation(int x)
			{
				return x + increment;
			}

			var itemsWhereThenSelected = items.Where(Filter).Select(Mutation);
			var itemsWhereSelected = items.WhereSelect(x => Filter(x) ? Option.Some(Mutation(x)) : Option.None<int>());

			Assert.Equal(itemsWhereThenSelected, itemsWhereSelected);
		}
	}

	public class OptionTests
	{
		private const bool SOME_INNER = true;
		private const bool NOT_SOME_INNER = !SOME_INNER;
		private static readonly Option<bool> _some = Option.Some(SOME_INNER);
		private static readonly Option<bool> _notSome = Option.Some(NOT_SOME_INNER);
		private static readonly Option<bool> _none = Option.None<bool>();

		[Fact]
		public void _Prerequisites()
		{
			Assert.NotEqual(SOME_INNER, NOT_SOME_INNER);
			Assert.NotEqual(_some, _notSome);
			Assert.NotEqual(_some, _none);
			Assert.NotEqual(_notSome, _none);
		}

		[Fact]
		public void IsSome()
		{
			Assert.True(_some.IsSome);
			Assert.False(_none.IsSome);
		}

		[Fact]
		public void IsNone()
		{
			Assert.False(_some.IsNone);
			Assert.True(_none.IsNone);
		}

		[Fact]
		public void Expect()
		{
			_some.Expect("This should not throw.");
			const string message = "This should throw.";
			var e = Assert.Throws<InvalidOperationException>(() => _none.Expect(message));

			Assert.Equal(message, e.Message);
		}

		[Fact]
		public void ExpectNone()
		{
			const string message = "This should throw.";
			var e = Assert.Throws<InvalidOperationException>(() => _some.ExpectNone(message));
			_none.ExpectNone("This should not throw.");

			Assert.Equal(message, e.Message);
		}

		[Fact]
		public void Unwrap()
		{
			var someUnwrapped = _some.Unwrap();
			Assert.Throws<InvalidOperationException>(() => _none.Unwrap());

			Assert.Equal(SOME_INNER, someUnwrapped);
		}

		[Fact]
		public void UnwrapNone()
		{
			Assert.Throws<InvalidOperationException>(() => _some.UnwrapNone());
			_none.UnwrapNone();
		}

		[Fact]
		public void UnwrapOr()
		{
			var someUnwrapped = _some.UnwrapOr(NOT_SOME_INNER);
			var noneUnwrapped = _none.UnwrapOr(NOT_SOME_INNER);

			Assert.Equal(SOME_INNER, someUnwrapped);
			Assert.Equal(NOT_SOME_INNER, noneUnwrapped);
		}

		[Fact]
		public void UnwrapOrElse()
		{
			var someUnwrapped = _some.UnwrapOrElse(() => throw new NotSupportedException());
			var noneUnwrapped = _none.UnwrapOrElse(() => NOT_SOME_INNER);

			Assert.Equal(SOME_INNER, someUnwrapped);
			Assert.Equal(NOT_SOME_INNER, noneUnwrapped);
		}

		[Fact]
		public void Or()
		{
			var someOred = _some.Or(_notSome);
			var noneOred = _none.Or(_notSome);

			Assert.Equal(_some, someOred);
			Assert.Equal(_notSome, noneOred);
		}

		[Fact]
		public void OrElse()
		{
			var someOred = _some.OrElse(() => throw new NotSupportedException());
			var noneOred = _none.OrElse(() => _notSome);

			Assert.Equal(_some, someOred);
			Assert.Equal(_notSome, noneOred);
		}

		[Fact]
		public void And()
		{
			var someAnded = _some.And(_notSome);
			var noneAnded = _none.And(_notSome);

			Assert.Equal(_notSome, someAnded);
			Assert.Equal(_none, noneAnded);
		}

		[Fact]
		public void Xor()
		{
			var someSomeXored = _some.Xor(_some);
			var noneSomeXored = _none.Xor(_some);
			var someNoneXored = _some.Xor(_none);
			var noneNoneXored = _none.Xor(_none);

			Assert.Equal(_some, noneSomeXored);
			Assert.Equal(_some, someNoneXored);
			Assert.Equal(_none, someSomeXored);
			Assert.Equal(_none, noneNoneXored);
		}

		[Fact]
		public void Filter()
		{
			var someFilteredFalse = _some.Filter(x =>
			{
				Assert.Equal(SOME_INNER, x);
				return false;
			});
			var someFilteredTrue = _some.Filter(x =>
			{
				Assert.Equal(SOME_INNER, x);
				return true;
			});
			var noneFiltered = _none.Filter(x => throw new NotSupportedException());

			Assert.Equal(_some, someFilteredTrue);
			Assert.Equal(_none, someFilteredFalse);
			Assert.Equal(_none, noneFiltered);
		}

		[Fact]
		public void Map()
		{
			var someMapped = _some.Map(x =>
			{
				Assert.Equal(SOME_INNER, x);
				return !x;
			});
			var noneMapped = _none.Map<bool>(x => throw new NotSupportedException());

			Assert.Equal(_notSome, someMapped);
			Assert.Equal(_none, noneMapped);
		}

		[Fact]
		public void MapAs()
		{
			var str = string.Empty;
			var some = Option.Some(str);
			var none = Option.None<string>();

			var someMapped = some.MapAs<object>();
			var noneMapped = none.MapAs<object>();
			
			Assert.Equal(Option.Some<object>(str), someMapped);
			Assert.True(noneMapped.IsNone);
		}

		[Fact]
		public void MapOr()
		{
			var someMapped = _some.MapOr(SOME_INNER, x =>
			{
				Assert.Equal(SOME_INNER, x);
				return !x;
			});
			var noneMapped = _none.MapOr(NOT_SOME_INNER, x => throw new NotSupportedException());

			Assert.Equal(NOT_SOME_INNER, someMapped);
			Assert.Equal(NOT_SOME_INNER, noneMapped);
		}

		[Fact]
		public void MapOrElse()
		{
			var someMapped = _some.MapOrElse(() => throw new NotSupportedException(), x =>
			{
				Assert.Equal(SOME_INNER, x);
				return NOT_SOME_INNER;
			});
			var noneMapped = _none.MapOrElse(() => NOT_SOME_INNER, x => throw new NotSupportedException());

			Assert.Equal(NOT_SOME_INNER, someMapped);
			Assert.Equal(NOT_SOME_INNER, noneMapped);
		}

		[Theory]
		[InlineData("")]
		[InlineData("a")]
		[InlineData(" a")]
		[InlineData("a ")]
		[InlineData(" a ")]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(true)]
		[InlineData(false)]
		public void _ToString_Some(object value)
		{
			var some = Option.Some(value);
			var str = some.ToString();

			Assert.Equal("Some(" + value + ")", str);
		}

		[Fact]
		public void _ToString_None()
		{
			var (noneBool, noneObject) = (Option.None<bool>(), Option.None<object>());
			var (boolStr, objectStr) = (noneBool.ToString(), noneObject.ToString());

			const string noneStr = "None";
			Assert.Equal(noneStr, boolStr);
			Assert.Equal(noneStr, objectStr);
		}
	}
}
