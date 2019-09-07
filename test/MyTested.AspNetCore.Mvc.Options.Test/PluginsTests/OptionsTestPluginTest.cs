namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Plugins;
    using Xunit;

    public class OptionsTestPluginTest
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new OptionsTestPlugin();

            Assert.Throws<NullReferenceException>(() => testPlugin.ServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new OptionsTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var methodReturnType = testPlugin.ServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(IOptions<>));
        }
    }
}
