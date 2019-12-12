namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Xunit;

    public class ViewDataTestPluginTests
    {
        [Fact]
        public void ShouldHavePriorityWithDefaultValue()
        {
            var testPlugin = new ViewDataTestPlugin();

            Assert.IsAssignableFrom<IDefaultRegistrationPlugin>(testPlugin);
            Assert.NotNull(testPlugin);
            Assert.Equal(-1000, testPlugin.Priority);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new ViewDataTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.DefaultServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new ViewDataTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.DefaultServiceRegistrationDelegate(serviceCollection);

            Assert.True(serviceCollection.Count == 157);        }
    }
}
