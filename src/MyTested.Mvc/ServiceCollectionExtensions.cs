namespace MyTested.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Extensions;
    using Utilities.Validators;

    public static class ServiceCollectionExtensions
    {
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
        {
            serviceCollection.TryRemoveTransient(typeof(TServive));
        }

        public static void TryRemoveTransient<TServive, TImplementation>(this IServiceCollection serviceCollection)
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
        {
            serviceCollection.TryRemoveSingleton(typeof(TServive));
        }

        public static void TryRemoveSingleton<TServive, TImplementation>(this IServiceCollection serviceCollection)
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
        {
            serviceCollection.TryRemoveScoped(typeof(TServive));
        }

        public static void TryRemoveScoped<TServive, TImplementation>(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryRemoveScoped(typeof(TServive), typeof(TImplementation));
        }

        public static void TryReplaceTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveTransient(service);
            serviceCollection.AddTransient(service, implementationType);
        }

        public static void TryReplaceTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplaceTransient(typeof(TService), typeof(TImplementation));
        }
        
        public static void TryReplaceSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationType);
        }

        public static void TryReplaceSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplaceSingleton(typeof(TService), typeof(TImplementation));
        }

        public static void TryReplaceScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveScoped(service);
            serviceCollection.AddScoped(service, implementationType);
        }

        public static void TryReplaceScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
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
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }
            
            serviceCollection
                .Where(predicate)
                .ToArray()
                .ForEach(s => serviceCollection.Remove(s));
        }
    }
}
