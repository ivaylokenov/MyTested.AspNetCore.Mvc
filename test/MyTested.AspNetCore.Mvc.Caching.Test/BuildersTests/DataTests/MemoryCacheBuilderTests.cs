namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using Internal.Caching;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class MemoryCacheBuilderTests : IDisposable
    {
        public MemoryCacheBuilderTests()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());
        }

        [Fact]
        public void WithEntryShouldSetCorrectValues()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntry("Normal", "NormalValid")
                    .AndAlso()
                    .WithEntry("Another", "AnotherValid"))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok()
                .WithModel("Normal");
        }

        [Fact]
        public void WithCacheOptionsShouldSetCorrectValues()
        {
            MyController<MemoryCacheController>
                .Instance()
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
                .WithModel(new MockedCacheEntry("FullEntry")
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
            MyController<MemoryCacheController>
                .Instance()
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
                .WithModel(new MockedCacheEntry("FullEntry")
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
                MyController<MemoryCacheController>
                    .Instance()
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
            MyController<MemoryCacheController>
                .Instance()
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
                .WithModel(new Dictionary<object, object>
                {
                    ["first"] = "firstValue",
                    ["second"] = "secondValue",
                    ["third"] = "thirdValue"
                });
        }

        public void Dispose()
        {
            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
