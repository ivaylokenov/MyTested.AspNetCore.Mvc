namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class DistributedCacheBuilderTests : IDisposable
    {
        public DistributedCacheBuilderTests()
        {
            MyApplication
               .StartsFrom<DefaultStartup>()
               .WithServices(services =>
               {
                   services.AddDistributedMemoryCache();

                   services
                       .AddMvc()
                       .PartManager
                       .ApplicationParts
                       .Add(new AssemblyPart(this.GetType().Assembly));
               });
        }

        [Fact]
        public void WithDistributedCacheShouldUseMock()
        {

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => { })
                .Calling(c => c.ValidDistributedCacheAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithEntryShouldSetCorrectValues()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry("FirstEntry", val)
                    .WithEntry("SecondEntry", val)
                    .WithEntry("ThirdEntry", val))
                .Calling(c => c.ValidDistributedCacheEntriesAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithEntryWithStringValueShouldSetCorrectValues()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry("FirstEntry", "FirstValue")
                    .WithEntry("SecondEntry", "SecondValue")
                    .WithEntry("ThirdEntry", "ThirdValue"))
                .Calling(c => c.ValidDistributedCacheEntriesAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithEntriesShouldSetCorrectValues()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntries(new Dictionary<string, byte[]>()
                    {
                        {"FirstEntry", val},
                        {"SecondEntry", val},
                        {"ThirdEntry", val},
                    }))
                .Calling(c => c.ValidDistributedCacheEntriesAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithEntriesWithStringValuesShouldSetCorrectValues()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntries(new Dictionary<string, string>()
                    {
                        {"FirstEntry", "FirstValue"},
                        {"SecondEntry", "SecondValue"},
                        {"ThirdEntry", "ThirdValue"},
                    }))
                .Calling(c => c.ValidDistributedCacheEntriesAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithEntryAndEntriesShouldSetCorrectValues()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry("FirstEntry", val)
                    .WithEntries(new Dictionary<string, byte[]>()
                    {
                        {"SecondEntry", val},
                        {"ThirdEntry", val},
                    }))
                .Calling(c => c.ValidDistributedCacheEntriesAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry("FirstEntry", val)
                    .AndAlso()
                    .WithEntry("SecondEntry", val)
                    .AndAlso()
                    .WithEntries(new Dictionary<string, byte[]>()
                    {
                        {"ThirdEntry", val},
                        {"FourthEntry", val},
                    }))
                .Calling(c => c.ValidDistributedCacheEntriesAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AndAlsoWithStringsShouldWorkCorrectly()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry("FirstEntry", "FirstValue")
                    .AndAlso()
                    .WithEntry("SecondEntry", "SecondValue")
                    .AndAlso()
                    .WithEntries(new Dictionary<string, string>()
                    {
                        {"ThirdEntry", "ThirdValue"},
                        {"FourthEntry", "FourthValue"},
                    }))
                .Calling(c => c.ValidDistributedCacheEntriesAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithCacheBuilderWithKeyShouldSetCorrectValues()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry(entry => entry
                        .WithKey("FirstEntry")))
                .Calling(c => c.ValidDistributedCacheEntryAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithCacheBuilderWithKeyWithValueShouldSetCorrectValues()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry(entry => entry
                        .WithKey("FirstEntry")
                        .WithValue(val)))
                .Calling(c => c.ValidDistributedCacheEntryAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithCacheBuilderWithKeyWithStringValueShouldSetCorrectValues()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry(entry => entry
                        .WithKey("FirstEntry")
                        .WithValue("FirstValue")))
                .Calling(c => c.ValidDistributedCacheEntryAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithCacheBuilderWithKeyBuilderShouldSetCorrectValues()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry(entry => entry
                        .WithKey("FirstEntry")
                        .WithValue(val)
                        .WithAbsoluteExpiration(DateTimeOffset.MaxValue)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.MaxValue)
                        .WithSlidingExpiration(TimeSpan.MaxValue)))
                .Calling(c => c.ValidDistributedCacheEntryAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithCacheBuilderWithKeyBuilderAndStringValueShouldSetCorrectValues()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry(entry => entry
                        .WithKey("FirstEntry")
                        .WithValue("FirstValue")
                        .WithAbsoluteExpiration(DateTimeOffset.MaxValue)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.MaxValue)
                        .WithSlidingExpiration(TimeSpan.MaxValue)))
                .Calling(c => c.ValidDistributedCacheEntryAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithCacheBuilderWithKeyBuilderAndAlsoShouldSetCorrectValues()
        {
            var val = new byte[] { 127, 127, 127 };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry(entry => entry
                        .WithKey("FirstEntry")
                        .AndAlso()
                        .WithValue(val)
                        .AndAlso()
                        .WithAbsoluteExpiration(DateTimeOffset.MaxValue)
                        .AndAlso()
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.MaxValue)
                        .AndAlso()
                        .WithSlidingExpiration(TimeSpan.MaxValue)))
                .Calling(c => c.ValidDistributedCacheEntryAction(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok();
        }

        public void Dispose() => MyApplication.StartsFrom<DefaultStartup>();
    }
}
