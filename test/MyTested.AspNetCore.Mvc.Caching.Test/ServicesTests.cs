namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Internal;
    using Internal.Caching;
    using Internal.Contracts;
    using Internal.Services;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Startups;
    using Setups.ViewComponents;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void MockMemoryCacheShouldBeRegisteredWithAddedCaching()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            Assert.IsAssignableFrom<MemoryCacheMock>(TestServiceProvider.GetService<IMemoryCache>());

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MockMemoryCacheShouldBeDifferentForEveryCallSynchronously()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            // second call should not have cache entries
            MyController<MvcController>
                .Instance()
                .WithMemoryCache(cache => cache.WithEntry("test", "value"))
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .Ok();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void MockMemoryCacheShouldBeDifferentForEveryViewComponentCallSynchronously()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            // second call should not have cache entries
            MyViewComponent<MemoryCacheComponent>
                .Instance()
                .WithMemoryCache(cache => cache.WithEntry("test", "value"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View();

            MyViewComponent<MemoryCacheComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("No cache");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MockMemoryCacheShouldBeDifferentForEveryCallSynchronouslyWithCachedControllerBuilder()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            var controller = new MyController<MvcController>();

            // second call should not have cache entries
            controller
                .WithMemoryCache(cache => cache.WithEntry("test", "value"))
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .Ok();

            controller
                .WithMemoryCache(cache => cache.WithEntry(string.Empty, string.Empty))
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DefaultConfigurationShouldSetMockMemoryCache()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<MemoryCacheMock>(memoryCache);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CustomMemoryCacheShouldOverrideTheMockOne()
        {
            MyApplication.StartsFrom<CachingDataStartup>();

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<CustomMemoryCache>(memoryCache);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ExplicitMockMemoryCacheShouldOverrideIt()
        {
            MyApplication
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceMemoryCache();
                });

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<MemoryCacheMock>(memoryCache);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MockMemoryCacheShouldBeDifferentForEveryCallAsynchronously()
        {
            Task
                .Run(async () =>
                {
                    MyApplication
                        .StartsFrom<DefaultStartup>()
                        .WithServices(services => services.AddMemoryCache());

                    TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
                    TestHelper.ExecuteTestCleanup();

                    string firstValue = null;
                    string secondValue = null;
                    string thirdValue = null;
                    string fourthValue = null;
                    string fifthValue = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "first");
                            firstValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "second");
                            secondValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "third");
                            thirdValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "fourth");
                            fourthValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "fifth");
                            fifthValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.Equal("first", firstValue);
                    Assert.Equal("second", secondValue);
                    Assert.Equal("third", thirdValue);
                    Assert.Equal("fourth", fourthValue);
                    Assert.Equal("fifth", fifthValue);

                    MyApplication.StartsFrom<DefaultStartup>();
                })
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public void MockDistributedCacheShouldBeRegisteredWithAddedCaching()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddDistributedMemoryCache());

            Assert.IsAssignableFrom<DistributedCacheMock>(TestServiceProvider.GetService<IDistributedCache>());

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MockDistributedCacheShouldBeDifferentForEveryCallSynchronously()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddDistributedMemoryCache());

            // second call should not have cache entries
            MyController<MvcController>
                .Instance()
                .WithDistributedCache(cache => cache.WithEntry("test", new byte[] { 127, 127,127 }))
                .Calling(c => c.DistributedCacheAction())
                .ShouldReturn()
                .Ok();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.DistributedCacheAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MockDistributedCacheShouldBeSameForEveryCallSynchronouslyWithCachedControllerBuilder()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddDistributedMemoryCache());

            var controller = new MyController<MvcController>();

            // second call should not have cache entries
            controller
                .WithDistributedCache(cache => cache.WithEntry("test", new byte[] { 127, 127,127 }))
                .Calling(c => c.DistributedCacheAction())
                .ShouldReturn()
                .Ok();

            controller
                .WithDistributedCache(cache => cache.WithEntry(string.Empty, new byte[] { }))
                .Calling(c => c.DistributedCacheAction())
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DefaultConfigurationShouldSetMockDistributedCache()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddDistributedMemoryCache());

            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();

            Assert.NotNull(distributedCache);
            Assert.IsAssignableFrom<DistributedCacheMock>(distributedCache);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CustomDistributedCacheShouldOverrideTheMockOne()
        {
            MyApplication.StartsFrom<CachingDataStartup>();

            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();

            Assert.NotNull(distributedCache);
            Assert.IsAssignableFrom<CustomDistributedCache>(distributedCache);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ExplicitMockDistributedCacheShouldOverrideIt()
        {
            MyApplication
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceDistributedCache();
                });

            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();

            Assert.NotNull(distributedCache);
            Assert.IsAssignableFrom<DistributedCacheMock>(distributedCache);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MockDistributedCacheShouldBeSameForAsyncronousCallToTestProvider()
        {
            Task
                .Run(async () =>
                {
                    MyApplication
                        .StartsFrom<DefaultStartup>()
                        .WithServices(services => services.AddDistributedMemoryCache());

                    var mockCache = TestServiceProvider.GetService<IDistributedCache>() as IDistributedCacheMock;
                    
                    TestHelper.GlobalTestCleanup += () => mockCache?.Dispose();
                    TestHelper.ExecuteTestCleanup();

                    byte[] firstValue = null;
                    byte[] secondValue = null;
                    byte[] thirdValue = null;
                    byte[] fourthValue = null;
                    byte[] fifthValue = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();
                            distributedCache.Set("test1", new byte[]{ 1 });
                            firstValue = TestServiceProvider.GetService<IDistributedCache>().Get("test1");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();
                            distributedCache.Set("test2", new byte[]{ 2 });
                            secondValue = TestServiceProvider.GetService<IDistributedCache>().Get("test2");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();
                            distributedCache.Set("test3", new byte[]{ 3 });
                            thirdValue = TestServiceProvider.GetService<IDistributedCache>().Get("test3");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();
                            distributedCache.Set("test4", new byte[]{ 4 });
                            fourthValue = TestServiceProvider.GetService<IDistributedCache>().Get("test4");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var distributedCache = TestServiceProvider.GetService<IDistributedCache>();
                            distributedCache.Set("test5", new byte[]{ 5 });
                            fifthValue = TestServiceProvider.GetService<IDistributedCache>().Get("test5");
                            TestHelper.ExecuteTestCleanup();
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.Equal(new byte[] { 1 }, firstValue);
                    Assert.Equal(new byte[] { 2 }, secondValue);
                    Assert.Equal(new byte[] { 3 }, thirdValue);
                    Assert.Equal(new byte[] { 4 }, fourthValue);
                    Assert.Equal(new byte[] { 5 }, fifthValue);

                    MyApplication.StartsFrom<DefaultStartup>();
                })
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
