namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using Exceptions;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class DistributedCacheEntryTestBuilderTests : IDisposable
    {
        public DistributedCacheEntryTestBuilderTests()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddDistributedMemoryCache());
        }

        [Fact]
        public void WithKeyShouldNotThrowExceptionWithCorrectKey()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(distributedCache => distributedCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithKeyShouldThrowExceptionWithIncorrectKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(distributedCache => distributedCache
                            .ContainingEntry(entry => entry
                                .WithKey("invalid")))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with the given key, but such was not found.");
        }

        [Fact]
        public void WithValueShouldNotThrowExceptionWithCorrectValue()
        {
            var val = new byte[] {127, 127, 127};

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(distributedCache => distributedCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithValue(val)))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    var invalidVal = new byte[] { 127, 128, 127 };
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(distributedCache => distributedCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithValue(invalidVal)))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with 'test' key and the given value, but in fact it was different.");
        }

        [Fact]
        public void WithAbsoluteExpirationShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(distributedCache => distributedCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithAbsoluteExpiration(new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc))))
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
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(distributedCache => distributedCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithAbsoluteExpiration(new DateTime(2010, 1, 2, 3, 4, 5, DateTimeKind.Utc))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with 'test' key and 'Sat, 02 Jan 2010 03:04:05 GMT' absolute expiration, but in fact found 'Wed, 01 Jan 2020 01:01:01 GMT'.");
        }

        [Fact]
        public void WithAbsoluteExpirationRelativeToNowShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(distributedCache => distributedCache
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
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(distributedCache => distributedCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(2))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with 'test' key and '00:02:00' absolute expiration relative to now, but in fact found '00:01:00'.");
        }

        [Fact]
        public void WithSlidingExpirationShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(distributedCache => distributedCache
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
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(distributedCache => distributedCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithSlidingExpiration(TimeSpan.FromMinutes(10))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected distributed cache to have an entry with 'test' key and '00:10:00' sliding expiration, but in fact found '00:05:00'.");
        }

        [Fact]
        public void WithNoMockMemoryCacheSomeMethodsShouldThrowException()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.Replace<IDistributedCache, CustomDistributedCache>(ServiceLifetime.Scoped);
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(distributedCache => distributedCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithSlidingExpiration(TimeSpan.FromMinutes(10))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "This test requires the registered IDistributedCache service to implement IDistributedCacheMock.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        public void Dispose() => MyApplication.StartsFrom<DefaultStartup>();
    }
}
