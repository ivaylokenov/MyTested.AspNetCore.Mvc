namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldHaveMemoryCacheTests
    {
        [Fact]
        public void NoMemoryCacheShouldNotThrowExceptionWithNoCacheEntries()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoMemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void NoMemoryCacheShouldThrowExceptionWithCacheEntries()
        {
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
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldNotThrowExceptionWithAnyCacheEntries()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldThrowExceptionWithNoCacheEntries()
        {
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
        }

        [Fact]
        public void MemoryCacheWithNumberShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidCacheEntries()
        {
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
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidManyCacheEntries()
        {
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
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry("test", "value"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
    }
}
