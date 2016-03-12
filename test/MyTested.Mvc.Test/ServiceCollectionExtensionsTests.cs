namespace MyTested.Mvc.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups.Services;
    using Xunit;

    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void NullServiceCollectionShouldThrowException()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                ServiceCollection serviceCollection = null;
                serviceCollection.TryRemoveTransient<IInjectedService>();
            });
        }

        [Fact]
        public void TryRemoveShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemove(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemove(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemove<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemove<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService>(s => new InjectedService());
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();
            serviceCollection.AddTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService>(s => new InjectedService());
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService>(s => new InjectedService());
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService, InjectedService>();
            serviceCollection.AddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService>(s => new InjectedService());
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService, InjectedService>();
            serviceCollection.AddScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService>(s => new InjectedService());
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService, InjectedService>();
            serviceCollection.AddScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService>(s => new InjectedService());
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplace(typeof(IInjectedService), typeof(ReplaceableInjectedService), ServiceLifetime.Singleton);

            Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService) && s.Lifetime == ServiceLifetime.Singleton));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var injectedService = new ReplaceableInjectedService();
            serviceCollection.TryReplace(typeof(IInjectedService), s => injectedService, ServiceLifetime.Singleton);

            var actualService = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService) && s.Lifetime == ServiceLifetime.Singleton);
            Assert.NotNull(actualService);

            Assert.Same(injectedService, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceShouldReplaceServiceByGenericType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplace<IInjectedService, ReplaceableInjectedService>(ServiceLifetime.Singleton);

            Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IInjectedService) && s.Lifetime == ServiceLifetime.Singleton));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceTransientShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }
        
        [Fact]
        public void TryReplaceTransientShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceTransient(typeof(IInjectedService), s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceTransientShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceTransientShouldReplaceServiceByGenericAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceTransient<IInjectedService>(s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }
        
        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceSingleton(typeof(IInjectedService), s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByTypeAndImplementationInstance()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceSingleton(typeof(IInjectedService), service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByGenericTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceSingleton<IInjectedService>(s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByGenericTypeAndImplementationInstance()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceSingleton<IInjectedService>(service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }
        
        [Fact]
        public void TryReplaceScopedShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }
        
        [Fact]
        public void TryReplaceScopedShouldReplaceServiceByTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceScoped(typeof(IInjectedService), s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceScopedShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceScopedShouldReplaceServiceByGenericTypeAndImplementationFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            var service = new ReplaceableInjectedService();
            serviceCollection.TryReplaceScoped<IInjectedService>(s => service);

            Assert.Same(service, serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceEnumerableShouldReplaceServiceWithOneArgument()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddEnumerable(
                ServiceDescriptor.Transient<IInjectedService, InjectedService>());

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());

            serviceCollection.TryReplaceEnumerable(
                ServiceDescriptor.Transient<IInjectedService, ReplaceableInjectedService>());

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());
        }

        [Fact]
        public void TryReplaceEnumerableShouldReplaceServiceWithMultipleArguments()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddEnumerable(
                ServiceDescriptor.Transient<IInjectedService, InjectedService>());

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());

            serviceCollection.TryReplaceEnumerable(new List<ServiceDescriptor>
            {
                ServiceDescriptor.Transient<IInjectedService, ReplaceableInjectedService>()
            });

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetServices<IInjectedService>().FirstOrDefault());
        }
    }
}
