namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        [Fact]
        public void WithoutDistributedCacheShouldReturnEmptyCache()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntries(new Dictionary<string, string>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .WithoutDistributedCache()
                .Calling(c => c.GetCount(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok.WithModel(0));
        }

        [Fact]
        public void WithoutDistributedCacheShouldReturnEmptyCacheWhenClearingAlreadyEmptyCache()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntries(new Dictionary<string, string>()))
                .WithoutDistributedCache()
                .Calling(c => c.GetCount(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok.WithModel(0));
        }

        [Fact]
        public void WithoutDistributedEntryCacheShouldReturnCorrectCacheData()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(cache => cache
                    .WithEntries(new Dictionary<string, string>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .WithoutDistributedCache(cache => cache.WithoutEntry("second"))
                .Calling(c => c.GetAllEntities(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new SortedDictionary<string, byte[]>
                    {
                        ["first"] = GetByteArray("firstValue"),
                        ["third"] = GetByteArray("thirdValue")
                    }));
        }

        [Fact]
        public void WithoutDistributedCacheByKeyShouldReturnCorrectCacheData()
        {
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(cache => cache
                    .WithEntries(new Dictionary<string, string>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .WithoutDistributedCache("second")
                .Calling(c => c.GetAllEntities(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new SortedDictionary<string, byte[]>
                    {
                        ["third"] = GetByteArray("thirdValue"),
                        ["first"] = GetByteArray("firstValue")
                    }));
        }

        [Fact]
        public void WithoutDistributedCacheByKeysShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<string, string>
            {
                ["first"] = "firstValue",
                ["second"] = "secondValue",
                ["third"] = "thirdValue"
            };

            var entriesToDelete = entities.Select(x => x.Key).ToList();
            entriesToDelete.RemoveAt(0);

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(cache => cache.WithEntries(entities))
                .WithoutDistributedCache(entriesToDelete)
                .Calling(c => c.GetAllEntities(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new SortedDictionary<string, byte[]>
                    {
                        ["first"] = GetByteArray("firstValue")
                    }));
        }

        [Fact]
        public void WithoutDistributedCacheByNonExistingKeysShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<string, byte[]>
            {
                ["first"] = GetByteArray("firstValue"),
                ["second"] = GetByteArray("secondValue"),
                ["third"] = GetByteArray("thirdValue")
            };

            var entitiesToDelete = new List<string> { "key1", "key2" };
            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(cache => cache
                    .WithEntries(entities))
                .WithoutDistributedCache(entitiesToDelete)
                .Calling(c => c.GetAllEntities(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new SortedDictionary<string, byte[]>(entities)));
        }

        [Fact]
        public void WithoutDistributedCacheByNonExistingKeyShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<string, byte[]>
            {
                ["first"] = GetByteArray("firstValue"),
                ["second"] = GetByteArray("secondValue"),
                ["third"] = GetByteArray("thirdValue")
            };

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(cache => cache
                    .WithEntries(entities))
                .WithoutDistributedCache("firstValue")
                .Calling(c => c.GetAllEntities(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new SortedDictionary<string, byte[]>(entities)));
        }

        [Fact]
        public void WithoutDistributedCacheByParamKeysShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<string, string>
            {
                ["first"] = "firstValue",
                ["second"] = "secondValue",
                ["third"] = "thirdValue"
            };

            var entriesToDelete = entities.Select(x => x.Key).ToList();
            entriesToDelete.RemoveAt(0);

            var encodedValue = GetByteArray("firstValue");

            MyController<DistributedCacheController>
                .Instance()
                .WithDistributedCache(cache => cache.WithEntries(entities))
                .WithoutDistributedCache(entriesToDelete[0], entriesToDelete[1])
                .Calling(c => c.GetAllEntities(From.Services<IDistributedCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new SortedDictionary<string, byte[]>
                    {
                        ["first"] = encodedValue
                    })); ;
        }

        public void Dispose() => MyApplication.StartsFrom<DefaultStartup>();

        private static byte[] GetByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}
