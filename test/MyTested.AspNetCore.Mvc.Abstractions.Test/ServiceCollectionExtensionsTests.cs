namespace MyTested.AspNetCore.Mvc.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups;
    using Setups.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.Services;
    using Xunit;

    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void NullServiceCollectionShouldThrowException()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                ServiceCollection serviceCollection = null;
                serviceCollection.RemoveTransient<IInjectedService>();
            });
        }

        [Fact]
        public void RemoveShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.Remove(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.Remove(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.Remove<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.Remove<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveTransientShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveTransient(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveTransientShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService>(s => new InjectedService());
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveTransientShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveTransient<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveTransientShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService>(s => new InjectedService());
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveSingletonShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveSingleton(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveSingletonShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService>(s => new InjectedService());
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveSingletonShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveSingleton<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveSingletonShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService>(s => new InjectedService());
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveScopedShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService, InjectedService>();
            serviceCollection.AddScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveScoped(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveScopedShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService>(s => new InjectedService());
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveScopedShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService, InjectedService>();
            serviceCollection.AddScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveScoped<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void RemoveScopedShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService>(s => new InjectedService());
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.RemoveScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.Replace(typeof(IInjectedService), typeof(ReplaceableInjectedService), ServiceLifetime.Singleton);

            Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService) && s.Lifetime == ServiceLifetime.Singleton));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var injectedService = new ReplaceableInjectedService();
            serviceCollection.Replace(typeof(IInjectedService), s => injectedService, ServiceLifetime.Singleton);

            var actualService = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService) && s.Lifetime == ServiceLifetime.Singleton);
            Assert.NotNull(actualService);

            Assert.Same(injectedService, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceShouldReplaceServiceByGenericType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.Replace<IInjectedService, ReplaceableInjectedService>(ServiceLifetime.Singleton);

            Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService) && s.Lifetime == ServiceLifetime.Singleton));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceTransientShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.ReplaceTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceTransientShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceTransient(typeof(IInjectedService), s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceTransientShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.ReplaceTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceTransientShouldReplaceServiceByGenericAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceTransient<IInjectedService>(s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceSingletonShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.ReplaceSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceSingletonShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceSingleton(typeof(IInjectedService), s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceSingletonShouldReplaceServiceByTypeAndImplementationInstance()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceSingleton(typeof(IInjectedService), service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceSingletonShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.ReplaceSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceSingletonShouldReplaceServiceByGenericTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceSingleton<IInjectedService>(s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceSingletonShouldReplaceServiceByGenericTypeAndImplementationInstance()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceSingleton<IInjectedService>(service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceScopedShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.ReplaceScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceScopedShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceScoped(typeof(IInjectedService), s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceScopedShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.ReplaceScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceScopedShouldReplaceServiceByGenericTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.ReplaceScoped<IInjectedService>(s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void ReplaceEnumerableShouldReplaceServiceWithOneArgument()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddEnumerable(
                ServiceDescriptor.Transient<IInjectedService, InjectedService>());

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());

            serviceCollection.ReplaceEnumerable(
                ServiceDescriptor.Transient<IInjectedService, ReplaceableInjectedService>());

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());
        }

        [Fact]
        public void ReplaceEnumerableShouldReplaceServiceWithMultipleArguments()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddEnumerable(
                ServiceDescriptor.Transient<IInjectedService, InjectedService>());

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());

            serviceCollection.ReplaceEnumerable(new List<ServiceDescriptor>
            {
                ServiceDescriptor.Transient<IInjectedService, ReplaceableInjectedService>()
            });

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());
        }

        [Fact]
        public void ReplaceLifetimeShouldWorkCorrectlyWithImplementationType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService, InjectedService>();

            Assert.Equal(ServiceLifetime.Singleton, serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService))?.Lifetime);

            serviceCollection.ReplaceLifetime<IInjectedService>(ServiceLifetime.Scoped);

            Assert.Single(serviceCollection);
            Assert.Equal(ServiceLifetime.Scoped, serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService))?.Lifetime);
        }

        [Fact]
        public void ReplaceLifetimeShouldWorkCorrectlyWithImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService>(s => new InjectedService());

            Assert.Equal(ServiceLifetime.Singleton, serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService))?.Lifetime);

            serviceCollection.ReplaceLifetime<IInjectedService>(ServiceLifetime.Scoped);

            Assert.Single(serviceCollection);
            Assert.Equal(ServiceLifetime.Scoped, serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService))?.Lifetime);
        }

        [Fact]
        public void AddCoreTestingShouldAddTestMarkerService()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMvc();
            serviceCollection.AddCoreTesting();

            Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(TestMarkerService)));
        }

        [Fact]
        public void AddCoreTestingShouldThrowExceptionIfAddMvcIsNotCalled()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    var serviceCollection = new ServiceCollection();
                    serviceCollection.AddCoreTesting();
                },
                "Unable to find the required services. Make sure you register the 'MyTested.AspNetCore.Mvc' testing infrastructure services after the web application ones.");
        }
    }
}
