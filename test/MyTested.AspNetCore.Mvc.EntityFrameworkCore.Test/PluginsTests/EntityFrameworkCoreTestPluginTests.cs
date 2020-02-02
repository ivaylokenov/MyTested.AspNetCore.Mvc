namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using Setups;
    using Setups.Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Xunit;

    public class EntityFrameworkCoreTestPluginTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new EntityFrameworkCoreTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.ServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollection()
        {
            var testPlugin = new EntityFrameworkCoreTestPlugin();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var methodReturnType = testPlugin.ServiceRegistrationDelegate.Method.ReturnType.Name;

            Assert.True(methodReturnType == "Void");
            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(DbContextOptions));
        }
    }
}
