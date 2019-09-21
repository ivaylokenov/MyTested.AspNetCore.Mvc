namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class DistributedCacheTestBuilderTests : IDisposable
    {
        public DistributedCacheTestBuilderTests()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddDistributedMemoryCache());
        }

        [Fact]
        public void ContainingEntryShouldNotThrowExceptionWithCorrectEntry()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntry("test", cacheValue))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntry()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntry("invalid", cacheValue))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with the given key, but such was not found.");
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldNotThrowExceptionWithCorrectEntry()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheActionWithStringValueEntries())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntry("test", "testValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheActionWithStringValueEntries())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntry("invalid", "bad"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheActionWithStringValueEntries action in MvcController expected distributed cache to have an entry with the given key, but such was not found.");
        }

        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowExceptionWithCorrectKey()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntryWithKey("test"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowExceptionWithIncorrectKey()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntryWithKey("invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with the given key, but such was not found.");
        }

        [Fact]
        public void ContainingEntryWithValueShouldNotThrowExceptionWithCorrectValue()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntryWithValue(cacheValue))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithValueShouldThrowExceptionWithIncorrectValue()
        {
            var cacheValue = new byte[] { 127, 128, 127 };

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntryWithValue(cacheValue))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with the given value, but such was not found.");
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheActionWithStringValueEntries())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntryWithValue("testValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheActionWithStringValueEntries())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntryWithValue("badValue"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheActionWithStringValueEntries action in MvcController expected distributed cache to have an entry with the given value, but such was not found.");
        }

        [Fact]
        public void ContainingEntryWithOptionsShouldNotThrowExceptionWithCorrectEntry()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntry("test", cacheValue, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = new DateTimeOffset(new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithOptionsShouldThrowExceptionWithIncorrectAbsoluteExpiration()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntry("test", cacheValue, new DistributedCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2040, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                                SlidingExpiration = TimeSpan.FromMinutes(5)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void ContainingEntryWithOptionsShouldThrowExceptionWithIncorrectAbsoluteExpirationRelativeToNow()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntry("test", cacheValue, new DistributedCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                                SlidingExpiration = TimeSpan.FromMinutes(5)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void ContainingEntryWithOptionsShouldThrowExceptionWithIncorrectSlidingExpiration()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntry("test", cacheValue, new DistributedCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                                SlidingExpiration = TimeSpan.FromMinutes(3)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void ContainingEntriesShouldNotThrowExceptionWithCorrectEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntries(new Dictionary<string, byte[]>
                    {
                        {"test", new byte[] { 127, 127, 127 } },
                        {"another", new byte[] { 4, 20} },
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithIncorrectFewEntriesCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntries(new Dictionary<string, byte[]>
                            {
                                {"test", new byte[] { 127, 127, 127 } }
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithIncorrectManyEntriesCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntries(new Dictionary<string, byte[]>
                            {
                                {"test", new byte[] { 127, 127, 127 } },
                                {"another", new byte[] { 4, 20} },
                                {"missing", new byte[] { 127, 0, 0, 1, } }
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void ContainingEntriesWithStringValuesShouldNotThrowExceptionWithCorrectEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheActionWithStringValueEntries())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntries(new Dictionary<string, string>
                    {
                        {"test", "testValue"},
                        {"another", "anotherValue"},
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntriesWithStringValuesShouldThrowExceptionWithIncorrectFewEntriesCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheActionWithStringValueEntries())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntries(new Dictionary<string, string>
                            {
                                {"test", "testValue"}
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheActionWithStringValueEntries action in MvcController expected distributed cache to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void ContainingEntriesWithStringValuesShouldThrowExceptionWithIncorrectManyEntriesCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheActionWithStringValueEntries())
                        .ShouldHave()
                        .DistributedCache(cache => cache
                            .ContainingEntries(new Dictionary<string, string>
                            {
                                {"test", "testValue"},
                                {"another", "anotherValue"},
                                {"missing", "missingValue"}
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheActionWithStringValueEntries action in MvcController expected distributed cache to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            var cacheValue = new byte[] { 127, 127, 127 };

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntryWithKey("test")
                    .AndAlso()
                    .ContainingEntryWithValue(cacheValue))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        public void Dispose() => MyApplication.StartsFrom<DefaultStartup>();
    }
}
