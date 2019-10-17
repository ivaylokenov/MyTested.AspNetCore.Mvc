namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Xunit;

    public class CachingTestPluginTest
    {
        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new CachingTestPlugin();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddMemoryCache();
            serviceCollection.AddDistributedMemoryCache();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            Assert.Contains(
                serviceCollection, 
                s => s.ServiceType == typeof(IMemoryCache) && s.ImplementationType == typeof(MemoryCache));

            Assert.Contains(
                serviceCollection, 
                s => s.ServiceType == typeof(IDistributedCache) && s.ImplementationType == typeof(MemoryDistributedCache));
        }
    }
}
