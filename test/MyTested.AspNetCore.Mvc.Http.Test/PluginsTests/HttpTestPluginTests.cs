using Microsoft.AspNetCore.Mvc.Controllers;

namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{  
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Plugins;
    using Xunit;

    public class HttpTestPluginTests
    {
        [Fact]
        public void ShouldHavePriorityWithDefaultValue()
        {
            var testPlugin = new HttpTestPlugin();

            Assert.IsAssignableFrom<IDefaultRegistrationPlugin>(testPlugin);
            Assert.NotNull(testPlugin);
            Assert.Equal(-9000, testPlugin.Priority);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new HttpTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.DefaultServiceRegistrationDelegate(null));
            Assert.Throws<NullReferenceException>(() => testPlugin.ServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollectionForDefaultRegistration()
        {
            var testPlugin = new HttpTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.DefaultServiceRegistrationDelegate(serviceCollection);

            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(IControllerFactory));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new HttpTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(IOptions<>));
        }
    }
}
