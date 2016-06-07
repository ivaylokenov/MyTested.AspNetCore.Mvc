namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
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
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoMemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void NoMemoryCacheShouldThrowExceptionWithCacheEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .NoMemoryCache()
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected to have memory cache with no entries, but in fact it had some.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldNotThrowExceptionWithAnyCacheEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldThrowExceptionWithNoCacheEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .MemoryCache()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have memory cache entries, but none were found.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void AsyncMemoryCacheActionShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheActionAsync())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 1)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidCacheEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .MemoryCache(1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have memory cache with 1 entry, but in fact contained 0.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidManyCacheEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(3)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected to have memory cache with 3 entries, but in fact contained 2.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldWorkCorrectly()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry("test", "value"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
