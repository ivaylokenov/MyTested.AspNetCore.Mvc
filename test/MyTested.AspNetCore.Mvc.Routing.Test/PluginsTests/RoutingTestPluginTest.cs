namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using Internal.Contracts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Xunit;

    public class RoutingTestPluginTest
    {
        [Fact]
        public void ShouldHavePriorityWithDefaultValue()
        {
            var testPlugin = new RoutingTestPlugin();

            Assert.IsAssignableFrom<IDefaultRegistrationPlugin>(testPlugin);
            Assert.NotNull(testPlugin);
            Assert.Equal(-8000, testPlugin.Priority);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new RoutingTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.DefaultServiceRegistrationDelegate(null));
            Assert.Throws<NullReferenceException>(() => testPlugin.RoutingServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollectionForDefaultRegistration()
        {
            var testPlugin = new RoutingTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.DefaultServiceRegistrationDelegate(serviceCollection);

            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(IControllerFactory));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new RoutingTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.RoutingServiceRegistrationDelegate(serviceCollection);

            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(IRoutingServices));
        }
    }
}
