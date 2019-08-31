namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using System;
    using Xunit;

    public class TempDataTestPluginTest
    {
        [Fact]
        public void ShouldHavePriorityWithDefaultValue()
        {
            var testPlugin = new TempDataTestPlugin();

            Assert.IsAssignableFrom<IDefaultRegistrationPlugin>(testPlugin);
            Assert.NotNull(testPlugin);
            Assert.Equal(-8000, testPlugin.Priority);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new TempDataTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.DefaultServiceRegistrationDelegate(null));
            Assert.Throws<NullReferenceException>(() => testPlugin.ServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollectionForDefaultRegistration()
        {
            var testPlugin = new TempDataTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.DefaultServiceRegistrationDelegate(serviceCollection);

            var methodReturnType = testPlugin.DefaultServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.True(serviceCollection.Count == 63);
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new TempDataTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var methodReturnType = testPlugin.ServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.True(serviceCollection.Count == 1);
        }
    }
}
