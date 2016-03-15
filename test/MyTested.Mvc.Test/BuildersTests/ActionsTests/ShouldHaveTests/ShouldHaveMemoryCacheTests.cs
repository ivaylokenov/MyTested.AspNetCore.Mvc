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
    }
}
