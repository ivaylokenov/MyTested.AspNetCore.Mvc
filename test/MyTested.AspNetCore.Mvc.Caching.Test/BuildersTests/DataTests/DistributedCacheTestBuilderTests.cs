namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
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
        public void ContainingEntryWithKeyShouldNotThrowExceptionWithCorrectEntry()
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
        public void ContainingEntryWithValueShouldNotThrowExceptionWithCorrectEntry()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(cache => cache
                    .ContainingEntryWithValue(val))
                .AndAlso()
                .ShouldReturn()
                .Ok();
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
        public void ContainingEntriesShouldNotThrowExceptionWithCorrectEntries()
        {
            var cacheValue = new byte[] { 127, 127, 127 };
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

        public void Dispose() => MyApplication.StartsFrom<DefaultStartup>();
    }
}
