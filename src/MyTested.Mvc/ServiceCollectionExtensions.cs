namespace MyTested.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Internal;
    using Internal.Caching;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Extensions;
    using Utilities.Validators;

    public static class ServiceCollectionExtensions
    {
        public static void AddHttpContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));
            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddActionContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
            serviceCollection.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
        
        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, params Type[] controllerTypes)
        {
            serviceCollection.AddMvcControllersAsServices(controllerTypes.AsEnumerable());
        }

        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, IEnumerable<Type> controllerTypes)
        {
            ControllersAsServices.AddControllersAsServices(serviceCollection, controllerTypes);
        }

        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, params Assembly[] controllerAssemblies)
        {
            serviceCollection.AddMvcControllersAsServices(controllerAssemblies.AsEnumerable());
        }

        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, IEnumerable<Assembly> controllerAssemblies)
        {
            ControllersAsServices.AddControllersAsServices(serviceCollection, controllerAssemblies);
        }

        public static void ReplaceTempDataProvider(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplaceSingleton<ITempDataProvider, MockedTempDataProvider>();
        }

        public static void ReplaceMemoryCache(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplace<IMemoryCache, MockedMemoryCache>(ServiceLifetime.Transient);
        }

        public static void ReplaceSession(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplaceTransient<ISessionStore, MockedSessionStore>();
        }

        public static void TryRemove(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service);
        }

        public static void TryRemove(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType);
        }

        public static void TryRemove<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemove(typeof(TServive));
        }

        public static void TryRemove<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemove(typeof(TServive), typeof(TImplementation));
        }

        public static void TryRemoveTransient(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Transient);
        }

        public static void TryRemoveTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Transient);
        }

        public static void TryRemoveTransient<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemoveTransient(typeof(TServive));
        }

        public static void TryRemoveTransient<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemoveTransient(typeof(TServive), typeof(TImplementation));
        }

        public static void TryRemoveSingleton(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Singleton);
        }

        public static void TryRemoveSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Singleton);
        }

        public static void TryRemoveSingleton<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemoveSingleton(typeof(TServive));
        }

        public static void TryRemoveSingleton<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemoveSingleton(typeof(TServive), typeof(TImplementation));
        }

        public static void TryRemoveScoped(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Scoped);
        }

        public static void TryRemoveScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Scoped);
        }

        public static void TryRemoveScoped<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemoveScoped(typeof(TServive));
        }

        public static void TryRemoveScoped<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemoveScoped(typeof(TServive), typeof(TImplementation));
        }

        public static void TryReplace(this IServiceCollection serviceCollection, Type service, Type implementationType, ServiceLifetime lifetime)
        {
            serviceCollection.TryRemove(service);
            serviceCollection.TryAdd(ServiceDescriptor.Describe(service, implementationType, lifetime));
        }

        public static void TryReplace(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            serviceCollection.TryRemove(service);
            serviceCollection.TryAdd(ServiceDescriptor.Describe(service, implementationFactory, lifetime));
        }

        public static void TryReplace<TService, TImplementation>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplace(typeof(TService), typeof(TImplementation), lifetime);
        }

        public static void TryReplaceTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveTransient(service);
            serviceCollection.AddTransient(service, implementationType);
        }

        public static void TryReplaceTransient(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.TryRemove(service);
            serviceCollection.AddTransient(service, implementationFactory);
        }

        public static void TryReplaceTransient<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.TryReplaceTransient(typeof(TService), implementationFactory);
        }

        public static void TryReplaceTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplaceTransient(typeof(TService), typeof(TImplementation));
        }

        public static void TryReplaceSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationType);
        }

        public static void TryReplaceSingleton(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.TryRemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationFactory);
        }

        public static void TryReplaceSingleton(this IServiceCollection serviceCollection, Type service, object implementationInstance)
        {
            serviceCollection.TryRemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationInstance);
        }

        public static void TryReplaceSingleton<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.TryReplaceSingleton(typeof(TService), implementationFactory);
        }

        public static void TryReplaceSingleton<TService>(this IServiceCollection serviceCollection, object implementationInstance)
            where TService : class
        {
            serviceCollection.TryReplaceSingleton(typeof(TService), implementationInstance);
        }

        public static void TryReplaceSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplaceSingleton(typeof(TService), typeof(TImplementation));
        }

        public static void TryReplaceScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveScoped(service);
            serviceCollection.AddScoped(service, implementationType);
        }

        public static void TryReplaceScoped(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.TryRemoveScoped(service);
            serviceCollection.AddScoped(service, implementationFactory);
        }

        public static void TryReplaceScoped<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.TryReplaceScoped(typeof(TService), implementationFactory);
        }

        public static void TryReplaceScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplaceScoped(typeof(TService), typeof(TImplementation));
        }

        public static void TryReplaceEnumerable(this IServiceCollection serviceCollection, ServiceDescriptor descriptor)
        {
            CommonValidator.CheckForNullReference(descriptor, nameof(descriptor));
            RemoveServices(serviceCollection, s => s.ServiceType == descriptor.ServiceType && s.Lifetime == descriptor.Lifetime);
            serviceCollection.TryAddEnumerable(descriptor);
        }

        public static void TryReplaceEnumerable(this IServiceCollection serviceCollection, IEnumerable<ServiceDescriptor> descriptors)
        {
            CommonValidator.CheckForNullReference(descriptors, nameof(descriptors));
            descriptors.ForEach(d => serviceCollection.TryReplaceEnumerable(d));
        }

        private static void RemoveServices(IServiceCollection serviceCollection, Func<ServiceDescriptor, bool> predicate)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));

            serviceCollection
                .Where(predicate)
                .ToArray()
                .ForEach(s => serviceCollection.Remove(s));
        }
    }
}
