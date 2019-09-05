namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using System;
    using System.Linq;
    using Xunit;

    public class CachingTestPluginTest
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new CachingTestPlugin();

            Assert.Throws<NullReferenceException>(() => testPlugin.ServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new CachingTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var methodReturnType = testPlugin.ServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(IMemoryCache));
        }
    }
}
