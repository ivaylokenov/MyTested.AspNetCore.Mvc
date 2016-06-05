namespace MyTested.Mvc.Test
{
    using Internal.Application;
    using Internal.Caching;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Caching.Memory;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Startups;
    using Xunit;

    public class MyMvcTests
    {
        [Fact]
        public void MockedMemoryCacheShouldBeRegistedByDefault()
        {
            MyMvc.IsUsingDefaultConfiguration();

            Assert.IsAssignableFrom<MockedMemoryCache>(TestServiceProvider.GetService<IMemoryCache>());
        }

        [Fact]
        public void MockedMemoryCacheShouldBeRegistedWithAddedCaching()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Assert.IsAssignableFrom<MockedMemoryCache>(TestServiceProvider.GetService<IMemoryCache>());

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MockedMemoryCacheShouldBeDifferentForEveryCallSynchronously()
        {
            // second call should not have cache entries
            MyMvc
                .Controller<MvcController>()
                .WithMemoryCache(cache => cache.WithEntry("test", "value"))
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .Ok();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void MockedMemoryCacheShouldBeDifferentForEveryCallSynchronouslyWithCachedControllerBuilder()
        {
            var controller = MyMvc.Controller<MvcController>();

            // second call should not have cache entries
            controller
                .WithMemoryCache(cache => cache.WithEntry("test", "value"))
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .Ok();

            controller
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void DefaultConfigurationShouldSetMockedMemoryCache()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<MockedMemoryCache>(memoryCache);
        }

        [Fact]
        public void CustomMemoryCacheShouldOverrideTheMockedOne()
        {
            MyMvc.StartsFrom<DataStartup>();

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<CustomMemoryCache>(memoryCache);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ExplicitMockedMemoryCacheShouldOverrideIt()
        {
            MyMvc
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceMemoryCache();
                });

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<MockedMemoryCache>(memoryCache);

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
