namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveMemoryCacheTests
    {
        [Fact]
        public void NoMemoryCacheShouldNotThrowExceptionWithNoCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoMemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void NoMemoryCacheShouldThrowExceptionWithCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldNotThrowExceptionWithAnyCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldThrowExceptionWithNoCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void AsyncMemoryCacheActionShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheActionAsync())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 1)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidManyCacheEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldWorkCorrectly()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
