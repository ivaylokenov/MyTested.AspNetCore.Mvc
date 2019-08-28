namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc.Plugins;
    using MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents;
    using Xunit;

    public class ViewComponentsTestPluginTests
    {
        [Fact]
        public void ShouldHavePriorityWithDefaultValue()
        {
            var testPlugin = new ViewComponentsTestPlugin();

            Assert.NotNull(testPlugin);
            Assert.IsAssignableFrom<IDefaultRegistrationPlugin>(testPlugin);
            Assert.Equal(-1000, testPlugin.Priority);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new ViewComponentsTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.DefaultServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new ViewComponentsTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.DefaultServiceRegistrationDelegate(serviceCollection);

            Assert.True(serviceCollection.Count == 145);
        }

        [Fact]
        public void TryGetValueShouldReturnNull()
        {
            var testPlugin = new ViewComponentsTestPlugin();
            var componentBuilder = new MyViewComponent<NormalComponent>();

            var result = testPlugin.TryGetValue(typeof(NormalComponent), componentBuilder.TestContext);

            Assert.Null(result);
        }
    }
}
