namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveMemoryCacheTests
    {
        [Fact]
        public void NoMemoryCacheShouldNotThrowExceptionWithViewComponentNoCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoMemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Content();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void NoMemoryCacheShouldThrowExceptionWithViewComponentCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .NoMemoryCache()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected to have memory cache with no entries, but in fact it had some.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldNotThrowExceptionWithViewComponentAnyCacheEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache()
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
