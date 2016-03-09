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
            Assert.Throws<ArgumentNullException>(() =>
            {
                ServiceCollection serviceCollection = null;
                serviceCollection.TryRemoveTransient<IInjectedService>();
            });
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddTransient<IInjectedService, InjectedService>();
            serviceCollection.TryAddTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddTransient<IInjectedService>(s => new InjectedService());
            serviceCollection.TryAddTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddTransient<IInjectedService, InjectedService>();
            serviceCollection.TryAddTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveTransientShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddTransient<IInjectedService>(s => new InjectedService());
            serviceCollection.TryAddTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<IInjectedService, InjectedService>();
            serviceCollection.TryAddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<IInjectedService>(s => new InjectedService());
            serviceCollection.TryAddSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<IInjectedService, InjectedService>();
            serviceCollection.TryAddSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveSingletonShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<IInjectedService>(s => new InjectedService());
            serviceCollection.TryAddSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByTypeOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddScoped<IInjectedService, InjectedService>();
            serviceCollection.TryAddScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped(typeof(IInjectedService));

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByTypeAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddScoped<IInjectedService>(s => new InjectedService());
            serviceCollection.TryAddScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByGenericOnlyInterface()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddScoped<IInjectedService, InjectedService>();
            serviceCollection.TryAddScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped<IInjectedService>();

            Assert.Null(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryRemoveScopedShouldRemoveServiceByGenericAndImplementation()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddScoped<IInjectedService>(s => new InjectedService());
            serviceCollection.TryAddScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.NotNull(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryRemoveScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceTransientShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddTransient(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceTransient(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceTransientShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddTransient<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceTransient<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceSingleton(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceSingletonShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceSingleton<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceScopedShouldReplaceServiceByType()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddScoped(typeof(IInjectedService), typeof(InjectedService));

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceScoped(typeof(IInjectedService), typeof(ReplaceableInjectedService));

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
        }

        [Fact]
        public void TryReplaceScopedShouldReplaceServiceByGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddScoped<IInjectedService, InjectedService>();

            Assert.IsAssignableFrom<InjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());

            serviceCollection.TryReplaceScoped<IInjectedService, ReplaceableInjectedService>();

            Assert.IsAssignableFrom<ReplaceableInjectedService>(serviceCollection.BuildServiceProvider().GetService<IInjectedService>());
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
