namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using Exceptions;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class MemoryCacheEntryTestBuilderTests : IDisposable
    {
        public MemoryCacheEntryTestBuilderTests()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());
        }

        [Fact]
        public void WithValidShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(memoryCache => memoryCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithValue("value")))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
        
        [Fact]
        public void WithValidShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(memoryCache => memoryCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithValue("invalid")))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with 'test' key and the given value, but in fact it was different. Expected a value of 'invalid', but in fact it was 'value'.");
        }
        
        [Fact]
        public void WithAbsoluteExpirationShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(memoryCache => memoryCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithAbsoluteExpiration(new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)))))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithAbsoluteExpirationShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(memoryCache => memoryCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithAbsoluteExpiration(new DateTimeOffset(new DateTime(2016, 2, 1, 1, 1, 1, DateTimeKind.Utc)))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with 'test' key and 'Mon, 01 Feb 2016 01:01:01 GMT' absolute expiration, but in fact found 'Fri, 01 Jan 2016 01:01:01 GMT'.");
        }

        [Fact]
        public void WithAbsoluteExpirationRelativeToNowShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(memoryCache => memoryCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(1))))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithAbsoluteExpirationRelativeToNowShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(memoryCache => memoryCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(2))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with 'test' key and '00:02:00' absolute expiration relative to now, but in fact found '00:01:00'.");
        }
        
        [Fact]
        public void WithPriorityShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(memoryCache => memoryCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithPriority(CacheItemPriority.High)))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithPriorityShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(memoryCache => memoryCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithPriority(CacheItemPriority.Low)))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with 'test' key and Low priority, but in fact found High.");
        }
        
        [Fact]
        public void WithSlidingExpirationShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(memoryCache => memoryCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithSlidingExpiration(TimeSpan.FromMinutes(5))))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithSlidingExpirationShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(memoryCache => memoryCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithSlidingExpiration(TimeSpan.FromMinutes(10))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with 'test' key and '00:10:00' sliding expiration, but in fact found '00:05:00'.");
        }

        [Fact]
        public void WithNoMockMemoryCacheSomeMethodsShouldThrowException()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.Replace<IMemoryCache, CustomMemoryCache>(ServiceLifetime.Transient);
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(memoryCache => memoryCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithSlidingExpiration(TimeSpan.FromMinutes(10))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "This test requires the registered IMemoryCache service to implement IMemoryCacheMock.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        public void Dispose()
        {
            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
