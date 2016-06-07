namespace MyTested.Mvc.Test.BuildersTests.DataTests
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
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());
        }

        [Fact]
        public void WithValidShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
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
        public void WithValidShouldThrowExceptionWithInvorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given value, but in fact it was different.");
        }
        
        [Fact]
        public void WithAbsoluteExpirationShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
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
        public void WithAbsoluteExpirationShouldThrowExceptionWithInvorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with '2/1/2016 1:01:01 AM +00:00' absolute expiration, but in fact found '1/1/2016 1:01:01 AM +00:00'.");
        }

        [Fact]
        public void WithAbsoluteExpirationRelativeToNowShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
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
        public void WithAbsoluteExpirationRelativeToNowShouldThrowExceptionWithInvorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with '00:02:00' absolute expiration relative to now, but in fact found '00:01:00'.");
        }
        
        [Fact]
        public void WithPriorityShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
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
        public void WithPriorityShouldThrowExceptionWithInvorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with Low priority, but in fact found High.");
        }
        
        [Fact]
        public void WithSlidingExpirationShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
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
        public void WithSlidingExpirationShouldThrowExceptionWithInvorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with '00:10:00' sliding expiration, but in fact found '00:05:00'.");
        }

        [Fact]
        public void WithNoMockedMemoryCacheSomeMethodsShouldThrowException()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddTransient<IMemoryCache, CustomMemoryCache>();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                "This test requires the registered IMemoryCache service to implement IMockedMemoryCache.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        public void Dispose()
        {
            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
