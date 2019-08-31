namespace MyTested.AspNetCore.Mvc.Test.PlungsTests
{
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using System;
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

            var methodReturnType = testPlugin.DefaultServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.True(serviceCollection.Count == 63);
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new RoutingTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.RoutingServiceRegistrationDelegate(serviceCollection);

            var methodReturnType = testPlugin.RoutingServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.True(serviceCollection.Count == 1);
        }
    }
}
