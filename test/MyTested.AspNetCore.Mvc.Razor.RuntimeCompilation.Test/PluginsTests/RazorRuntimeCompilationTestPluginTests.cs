namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using System.Linq;
    using Internal.Razor;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Setups;
    using Xunit;

    public class RazorRuntimeCompilationTestPluginTests
    {
        [Fact]
        public void ServiceSelectorPredicateShouldReturnTrueWithActionDescriptorChangeProvider()
        {
            var testPlugin = new RazorRuntimeCompilationTestPlugin();

            var serviceDescriptor = ServiceDescriptor.Singleton<IActionDescriptorChangeProvider, TestActionDescriptorChangeProvider>();

            Assert.True(testPlugin.ServiceSelectorPredicate(serviceDescriptor));
        }

        [Fact]
        public void ServiceSelectorPredicateShouldReturnFalseWithOtherServices()
        {
            var testPlugin = new RazorRuntimeCompilationTestPlugin();

            var serviceDescriptor = ServiceDescriptor.Singleton<IActionDescriptorProvider, PageActionDescriptorProvider>();

            Assert.False(testPlugin.ServiceSelectorPredicate(serviceDescriptor));
        }

        [Fact]
        public void ShouldThrowNullReferenceExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new RazorRuntimeCompilationTestPlugin();

            Test.AssertException<NullReferenceException>(
                () => testPlugin.ServiceRegistrationDelegate(null),
                "IServiceCollection cannot be null.");
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldReplaceRazorRuntimeCompilationServices()
        {
            var testPlugin = new RazorRuntimeCompilationTestPlugin();
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            serviceCollection
                .AddRazorPages();

            Assert.Contains(serviceCollection, IsPageActionDescriptorProvider);
            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(IActionDescriptorChangeProvider));

            var actionDescriptorProvidersCount = serviceCollection.Count(s => s.ServiceType == typeof(IActionDescriptorProvider));

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            Assert.DoesNotContain(serviceCollection, IsPageActionDescriptorProvider);
            Assert.Equal(actionDescriptorProvidersCount - 1, serviceCollection.Count(s => s.ServiceType == typeof(IActionDescriptorProvider)));

            var actionDescriptorChangeProvider = Assert.Single(serviceCollection, s => s.ServiceType == typeof(IActionDescriptorChangeProvider));

            Assert.Equal(typeof(TestActionDescriptorChangeProvider), actionDescriptorChangeProvider.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, actionDescriptorChangeProvider.Lifetime);
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldRegisterActionDescriptorChangeProviderWhichNeverChanges()
        {
            var testPlugin = new RazorRuntimeCompilationTestPlugin();
            var serviceCollection = new ServiceCollection();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var actionDescriptorChangeProvider = Assert.Single(serviceProvider.GetServices<IActionDescriptorChangeProvider>());

            Assert.IsType<TestActionDescriptorChangeProvider>(actionDescriptorChangeProvider);

            var changeToken = actionDescriptorChangeProvider.GetChangeToken();

            Assert.False(changeToken.HasChanged);
            Assert.False(changeToken.ActiveChangeCallbacks);
            Assert.NotNull(changeToken.RegisterChangeCallback(_ => { }, null));
        }

        private static bool IsPageActionDescriptorProvider(ServiceDescriptor serviceDescriptor)
            => serviceDescriptor.ServiceType == typeof(IActionDescriptorProvider)
                && serviceDescriptor.ImplementationType == typeof(PageActionDescriptorProvider);
    }
}
