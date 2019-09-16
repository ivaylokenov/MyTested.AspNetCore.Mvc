namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Exceptions;
    using Test.Setups;
    using Test.Setups.Controllers;
    using Xunit;

    public class ShouldHaveDistributedCacheTests : IDisposable
    {
        public ShouldHaveDistributedCacheTests()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddDistributedMemoryCache());
        }

        [Fact]
        public void NoDistributedCacheShouldNotThrowExceptionWithNoCacheEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoDistributedCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void NoDistributedCacheShouldThrowExceptionWithCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .NoDistributedCache()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected to have distributed cache with no entries, but in fact it had some.");
        }

        [Fact]
        public void DistributedCacheWithNoNumberShouldNotThrowExceptionWithAnyCacheEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void DistributedCacheWithNoNumberShouldThrowExceptionWithNoCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .DistributedCache()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have distributed cache entries, but none were found.");
        }

        [Fact]
        public void DistributedCacheWithNumberShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheAction())
                .ShouldHave()
                .DistributedCache(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AsyncDistributedCacheActionShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddDistributedCacheActionAsync())
                .ShouldHave()
                .DistributedCache(withNumberOfEntries: 1)
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void DistributedCacheWithNumberShouldThrowExceptionWithInvalidCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .DistributedCache(withNumberOfEntries: 1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have distributed cache with 1 entry, but in fact contained 0.");
        }

        [Fact]
        public void DistributedCacheWithNumberShouldThrowExceptionWithInvalidManyCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddDistributedCacheAction())
                        .ShouldHave()
                        .DistributedCache(3)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddDistributedCacheAction action in MvcController expected to have distributed cache with 3 entries, but in fact contained 2.");
        }

        public void Dispose()
        {
            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
