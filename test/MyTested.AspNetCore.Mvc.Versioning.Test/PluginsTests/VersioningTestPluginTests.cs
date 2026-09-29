namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using System;
    using System.Linq;
    using Asp.Versioning;
    using Internal.Versioning;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Setups.Common;
    using Xunit;

    public class VersioningTestPluginTests
    {
        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollectionForDefaultRegistration()
        {
            var testPlugin = new VersioningTestPlugin();

            var httpContext = new DefaultHttpContext();

            testPlugin.HttpFeatureRegistrationDelegate(httpContext);

            var versioningFeature = httpContext.Features[typeof(IApiVersioningFeature)];

            Assert.NotNull(versioningFeature);
            Assert.IsType<ApiVersioningFeature>(versioningFeature);
        }

        [Fact]
        public void HttpFeatureRegistrationDelegateShouldNotReplaceExistingApiVersioningFeature()
        {
            var testPlugin = new VersioningTestPlugin();

            var httpContext = new DefaultHttpContext();
            var apiVersioningFeature = new ApiVersioningFeature(httpContext);

            httpContext.Features.Set<IApiVersioningFeature>(apiVersioningFeature);

            testPlugin.HttpFeatureRegistrationDelegate(httpContext);

            Assert.Same(apiVersioningFeature, httpContext.Features.Get<IApiVersioningFeature>());
        }

        [Fact]
        public void ServiceSelectorPredicateShouldReturnTrueWithApiVersionParser()
        {
            var testPlugin = new VersioningTestPlugin();

            var serviceDescriptor = ServiceDescriptor.Singleton<IApiVersionParser>(ApiVersionParser.Default);

            Assert.True(testPlugin.ServiceSelectorPredicate(serviceDescriptor));
        }

        [Fact]
        public void ServiceSelectorPredicateShouldReturnFalseWithOtherServices()
        {
            var testPlugin = new VersioningTestPlugin();

            var actionSelectorServiceDescriptor = ServiceDescriptor.Singleton<IActionSelector, CustomActionSelector>();
            var apiVersionReaderServiceDescriptor = ServiceDescriptor.Singleton<IApiVersionReader>(new QueryStringApiVersionReader());

            Assert.False(testPlugin.ServiceSelectorPredicate(actionSelectorServiceDescriptor));
            Assert.False(testPlugin.ServiceSelectorPredicate(apiVersionReaderServiceDescriptor));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithInvalidServiceCollection()
        {
            var testPlugin = new VersioningTestPlugin();

            Assert.Throws<ArgumentNullException>(() => testPlugin.ServiceRegistrationDelegate(null));
            Assert.Throws<ArgumentNullException>(() => testPlugin.RoutingServiceRegistrationDelegate(null));
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldDecorateTheMvcActionSelectorInPlace()
        {
            var testPlugin = new VersioningTestPlugin();

            AssertDecoratedMvcActionSelector(testPlugin.ServiceRegistrationDelegate);
        }

        [Fact]
        public void RoutingServiceRegistrationDelegateShouldDecorateTheMvcActionSelectorInPlace()
        {
            var testPlugin = new VersioningTestPlugin();

            AssertDecoratedMvcActionSelector(testPlugin.RoutingServiceRegistrationDelegate);
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldNotDecorateActionSelectorWithoutApiVersioning()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IActionSelector, CustomActionSelector>();

            var actionSelectorServiceDescriptor = serviceCollection[0];

            testPlugin.ServiceRegistrationDelegate(serviceCollection);
            testPlugin.RoutingServiceRegistrationDelegate(serviceCollection);

            Assert.Same(actionSelectorServiceDescriptor, Assert.Single(serviceCollection));
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldNotChangeServicesWithoutActionSelector()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddApiVersioning();

            var serviceDescriptors = serviceCollection.ToArray();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);
            testPlugin.RoutingServiceRegistrationDelegate(serviceCollection);

            Assert.Equal(serviceDescriptors, serviceCollection);
            Assert.DoesNotContain(serviceCollection, s => s.ServiceType == typeof(IActionSelector));
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldNotDecorateActionSelectorTwice()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IActionSelector, CustomActionSelector>();
            serviceCollection.AddApiVersioning();

            var actionSelectorServiceDescriptor = serviceCollection[0];

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var decoratedServiceDescriptor = serviceCollection[0];

            testPlugin.ServiceRegistrationDelegate(serviceCollection);
            testPlugin.RoutingServiceRegistrationDelegate(serviceCollection);

            Assert.Same(decoratedServiceDescriptor, serviceCollection[0]);

            var apiVersionAwareServiceDescriptor = Assert.IsType<ApiVersionAwareActionSelectorServiceDescriptor>(decoratedServiceDescriptor);

            Assert.Same(actionSelectorServiceDescriptor, apiVersionAwareServiceDescriptor.ActionSelectorServiceDescriptor);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var actionSelector = Assert.IsType<ApiVersionAwareActionSelector>(serviceProvider.GetRequiredService<IActionSelector>());

            Assert.IsType<CustomActionSelector>(actionSelector.ActionSelector);
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldResolveTypeRegisteredActionSelector()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IActionSelector, CustomActionSelector>();
            serviceCollection.AddApiVersioning();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var serviceDescriptor = Assert.IsType<ApiVersionAwareActionSelectorServiceDescriptor>(serviceCollection[0]);

            Assert.Equal(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var actionSelector = serviceProvider.GetRequiredService<IActionSelector>();
            var apiVersionAwareActionSelector = Assert.IsType<ApiVersionAwareActionSelector>(actionSelector);

            Assert.IsType<CustomActionSelector>(apiVersionAwareActionSelector.ActionSelector);
            Assert.Same(actionSelector, serviceProvider.GetRequiredService<IActionSelector>());
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldResolveInstanceRegisteredActionSelector()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();
            var customActionSelector = new CustomActionSelector();

            serviceCollection.AddSingleton<IActionSelector>(customActionSelector);
            serviceCollection.AddApiVersioning();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var serviceDescriptor = Assert.IsType<ApiVersionAwareActionSelectorServiceDescriptor>(serviceCollection[0]);

            Assert.Equal(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var actionSelector = Assert.IsType<ApiVersionAwareActionSelector>(serviceProvider.GetRequiredService<IActionSelector>());

            Assert.Same(customActionSelector, actionSelector.ActionSelector);
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldResolveFactoryRegisteredActionSelector()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();
            var factoryCalls = 0;

            serviceCollection.AddTransient<IActionSelector>(_ =>
            {
                factoryCalls++;
                return new CustomActionSelector();
            });

            serviceCollection.AddApiVersioning();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var serviceDescriptor = Assert.IsType<ApiVersionAwareActionSelectorServiceDescriptor>(serviceCollection[0]);

            Assert.Equal(ServiceLifetime.Transient, serviceDescriptor.Lifetime);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var firstActionSelector = Assert.IsType<ApiVersionAwareActionSelector>(serviceProvider.GetRequiredService<IActionSelector>());
            var secondActionSelector = Assert.IsType<ApiVersionAwareActionSelector>(serviceProvider.GetRequiredService<IActionSelector>());

            Assert.IsType<CustomActionSelector>(firstActionSelector.ActionSelector);
            Assert.IsType<CustomActionSelector>(secondActionSelector.ActionSelector);
            Assert.NotSame(firstActionSelector.ActionSelector, secondActionSelector.ActionSelector);
            Assert.Equal(2, factoryCalls);
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldKeepScopedLifetimeOfActionSelector()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IActionSelector, CustomActionSelector>();
            serviceCollection.AddApiVersioning();

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            var serviceDescriptor = Assert.IsType<ApiVersionAwareActionSelectorServiceDescriptor>(serviceCollection[0]);

            Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);

            using var serviceProvider = serviceCollection.BuildServiceProvider();
            using var firstScope = serviceProvider.CreateScope();
            using var secondScope = serviceProvider.CreateScope();

            var firstActionSelector = firstScope.ServiceProvider.GetRequiredService<IActionSelector>();
            var secondActionSelector = secondScope.ServiceProvider.GetRequiredService<IActionSelector>();

            Assert.IsType<ApiVersionAwareActionSelector>(firstActionSelector);
            Assert.Same(firstActionSelector, firstScope.ServiceProvider.GetRequiredService<IActionSelector>());
            Assert.NotSame(firstActionSelector, secondActionSelector);
        }

        [Fact]
        public void ServiceRegistrationDelegateShouldDecorateOnlyTheLastNonKeyedActionSelector()
        {
            var testPlugin = new VersioningTestPlugin();
            var serviceCollection = new ServiceCollection();

            var firstActionSelector = new CustomActionSelector();
            var secondActionSelector = new CustomActionSelector();
            var keyedActionSelector = new CustomActionSelector();

            serviceCollection.AddSingleton<IActionSelector>(firstActionSelector);
            serviceCollection.AddSingleton<IActionSelector>(secondActionSelector);
            serviceCollection.AddKeyedSingleton<IActionSelector>("Keyed", keyedActionSelector);
            serviceCollection.AddApiVersioning();

            var firstServiceDescriptor = serviceCollection[0];
            var secondServiceDescriptor = serviceCollection[1];
            var keyedServiceDescriptor = serviceCollection[2];

            testPlugin.ServiceRegistrationDelegate(serviceCollection);

            Assert.Same(firstServiceDescriptor, serviceCollection[0]);
            Assert.Same(keyedServiceDescriptor, serviceCollection[2]);

            var decoratedServiceDescriptor = Assert.IsType<ApiVersionAwareActionSelectorServiceDescriptor>(serviceCollection[1]);

            Assert.Same(secondServiceDescriptor, decoratedServiceDescriptor.ActionSelectorServiceDescriptor);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var actionSelector = Assert.IsType<ApiVersionAwareActionSelector>(serviceProvider.GetRequiredService<IActionSelector>());

            Assert.Same(secondActionSelector, actionSelector.ActionSelector);
            Assert.Same(keyedActionSelector, serviceProvider.GetRequiredKeyedService<IActionSelector>("Keyed"));
        }

        private static void AssertDecoratedMvcActionSelector(Action<IServiceCollection> serviceRegistrationDelegate)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddControllers();
            serviceCollection.AddApiVersioning().AddMvc();

            var serviceCount = serviceCollection.Count;
            var actionSelectorIndex = serviceCollection.IndexOf(serviceCollection.Last(s => s.ServiceType == typeof(IActionSelector)));
            var actionSelectorServiceDescriptor = serviceCollection[actionSelectorIndex];

            serviceRegistrationDelegate(serviceCollection);

            Assert.Equal(serviceCount, serviceCollection.Count);
            Assert.Single(serviceCollection, s => s.ServiceType == typeof(IActionSelector));

            var decoratedServiceDescriptor = Assert.IsType<ApiVersionAwareActionSelectorServiceDescriptor>(serviceCollection[actionSelectorIndex]);

            Assert.Same(actionSelectorServiceDescriptor, decoratedServiceDescriptor.ActionSelectorServiceDescriptor);
            Assert.Equal(actionSelectorServiceDescriptor.Lifetime, decoratedServiceDescriptor.Lifetime);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var actionSelector = Assert.IsType<ApiVersionAwareActionSelector>(serviceProvider.GetRequiredService<IActionSelector>());

            Assert.IsType(actionSelectorServiceDescriptor.ImplementationType, actionSelector.ActionSelector);
        }
    }
}
