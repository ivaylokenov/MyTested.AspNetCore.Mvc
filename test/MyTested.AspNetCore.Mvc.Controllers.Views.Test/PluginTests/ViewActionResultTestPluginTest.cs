namespace MyTested.AspNetCore.Mvc.Test.PluginTests
{ 
    using System;
    using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Xunit;

    public class ViewActionResultTestPluginTest
    {
        [Fact]
        public void ShouldHavePriorityWithDefaultValue()
        {
            var testPlugin = new ViewActionResultsTestPlugin();

            Assert.IsAssignableFrom<IDefaultRegistrationPlugin>(testPlugin);
            Assert.NotNull(testPlugin);
            Assert.Equal(-1000, testPlugin.Priority);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new ViewActionResultsTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.DefaultServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new ViewActionResultsTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.DefaultServiceRegistrationDelegate(serviceCollection);

            var methodReturnType = testPlugin.DefaultServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(JsonResultExecutor));
        }
    }
}
