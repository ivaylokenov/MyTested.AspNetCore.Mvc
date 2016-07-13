namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldHaveMemoryCacheTests
    {
        [Fact]
        public void NoMemoryCacheShouldNotThrowExceptionWithNoCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoMemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void NoMemoryCacheShouldThrowExceptionWithCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .NoMemoryCache()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected to have memory cache with no entries, but in fact it had some.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldNotThrowExceptionWithAnyCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldThrowExceptionWithNoCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .MemoryCache()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have memory cache entries, but none were found.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void AsyncMemoryCacheActionShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheActionAsync())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 1)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .MemoryCache(1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have memory cache with 1 entry, but in fact contained 0.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidManyCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(3)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected to have memory cache with 3 entries, but in fact contained 2.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldWorkCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry("test", "value"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
