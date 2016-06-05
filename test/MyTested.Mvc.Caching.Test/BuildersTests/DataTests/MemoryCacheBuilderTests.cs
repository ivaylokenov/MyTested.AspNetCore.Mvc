namespace MyTested.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using Internal.Caching;
    using Microsoft.Extensions.Caching.Memory;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class MemoryCacheBuilderTests
    {
        [Fact]
        public void WithEntryShouldSetCorrectValues()
        {
            MyMvc
                .Controller<MemoryCacheController>()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntry("Normal", "NormalValid")
                    .AndAlso()
                    .WithEntry("Another", "AnotherValid"))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok()
                .WithResponseModel("Normal");
        }

        [Fact]
        public void WithCacheOptionsShouldSetCorrectValues()
        {
            MyMvc
                .Controller<MemoryCacheController>()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntry("FullEntry", "FullEntryValid", new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    }))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(new MockedCacheEntry("FullEntry")
                {
                    Value = "FullEntryValid",
                    AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
        }

        [Fact]
        public void WithCacheBuilderShouldSetCorrectValues()
        {
            MyMvc
                .Controller<MemoryCacheController>()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntry(entry => entry
                        .WithKey("FullEntry")
                        .AndAlso()
                        .WithValue("FullEntryValid")
                        .AndAlso()
                        .WithAbsoluteExpiration(new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)))
                        .AndAlso()
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(1))
                        .AndAlso()
                        .WithPriority(CacheItemPriority.High)
                        .AndAlso()
                        .WithSlidingExpiration(TimeSpan.FromMinutes(5))))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(new MockedCacheEntry("FullEntry")
                {
                    Value = "FullEntryValid",
                    AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
        }

        [Fact]
        public void WithCacheBuilderWithoutKeyShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(() =>
            {
                MyMvc
                    .Controller<MemoryCacheController>()
                    .WithMemoryCache(memoryCache => memoryCache
                        .WithEntry(entry => entry.WithValue("WithoutKey")))
                    .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                    .ShouldReturn()
                    .Ok();
            },
            "Cache entry key must be provided. 'WithKey' method must be called on the memory cache entry builder in order to run this test case successfully.");
        }

        [Fact]
        public void WithCacheEntriesShouldSetCorrectValues()
        {
            MyMvc
                .Controller<MemoryCacheController>()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(new Dictionary<object, object>
                {
                    ["first"] = "firstValue",
                    ["second"] = "secondValue",
                    ["third"] = "thirdValue"
                });
        }
    }
}
