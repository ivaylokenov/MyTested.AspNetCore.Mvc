namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Provides useful <see cref="IServiceCollection"/> extensions for testing purposes.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="IHttpContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));
            return serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds <see cref="IActionContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddActionContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
            return serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        /// <summary>
        /// Removes а service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Remove(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Remove(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Remove<TService>(this IServiceCollection serviceCollection)
            where TService : class
        {
            return serviceCollection.Remove(typeof(TService));
        }

        /// <summary>
        /// Removes a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Remove<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.Remove(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveTransient(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Transient);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Transient);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveTransient<TService>(this IServiceCollection serviceCollection)
            where TService : class
        {
            return serviceCollection.RemoveTransient(typeof(TService));
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.RemoveTransient(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveSingleton(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Singleton);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Singleton);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveSingleton<TService>(this IServiceCollection serviceCollection)
            where TService : class
        {
            return serviceCollection.RemoveSingleton(typeof(TService));
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.RemoveSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveScoped(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Scoped);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Scoped);
            return serviceCollection;
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveScoped<TService>(this IServiceCollection serviceCollection)
            where TService : class
        {
            return serviceCollection.RemoveScoped(typeof(TService));
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RemoveScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.RemoveScoped(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Replaces a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Replace(this IServiceCollection serviceCollection, Type service, Type implementationType, ServiceLifetime lifetime)
        {
            serviceCollection.Remove(service).Add(ServiceDescriptor.Describe(service, implementationType, lifetime));
            TestServiceProvider.SaveServiceLifetime(service, lifetime);
            return serviceCollection;
        }

        /// <summary>
        /// Replaces a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Replace<TService, TImplementation>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.Replace(typeof(TService), typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// Replaces a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Replace(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            serviceCollection.Remove(service).Add(ServiceDescriptor.Describe(service, implementationFactory, lifetime));
            TestServiceProvider.SaveServiceLifetime(service, lifetime);
            return serviceCollection;
        }

        /// <summary>
        /// Replaces a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection Replace<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
            where TService : class
        {
            return serviceCollection.Replace(typeof(TService), implementationFactory, lifetime);
        }

        /// <summary>
        /// Replaces service lifetime in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceLifetime(this IServiceCollection serviceCollection, Type service, ServiceLifetime lifetime)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection
                .Where(s => s.ServiceType == service)
                .ToArray()
                .ForEach(s =>
                {
                    if (s.ImplementationType != null)
                    {
                        serviceCollection.Replace(s.ServiceType, s.ImplementationType, lifetime);
                    }
                    else if (s.ImplementationFactory != null)
                    {
                        serviceCollection.Replace(s.ServiceType, s.ImplementationFactory, lifetime);
                    }
                });

            return serviceCollection;
        }

        /// <summary>
        /// Replaces service lifetime in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceLifetime<TService>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TService : class
        {
            return serviceCollection.ReplaceLifetime(typeof(TService), lifetime);
        }
        
        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            return serviceCollection
                .RemoveTransient(service)
                .AddTransient(service, implementationType);
        }

        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceTransient(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            return serviceCollection
                .RemoveTransient(service)
                .AddTransient(service, implementationFactory);
        }

        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceTransient<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            return serviceCollection.ReplaceTransient(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.ReplaceTransient(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            return serviceCollection
                .RemoveSingleton(service)
                .AddSingleton(service, implementationType);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceSingleton(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            return serviceCollection
                .RemoveSingleton(service)
                .AddSingleton(service, implementationFactory);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceSingleton(this IServiceCollection serviceCollection, Type service, object implementationInstance)
        {
            return serviceCollection
                .RemoveSingleton(service)
                .AddSingleton(service, implementationInstance);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            return serviceCollection.ReplaceSingleton(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection serviceCollection, object implementationInstance)
            where TService : class
        {
            return serviceCollection.ReplaceSingleton(typeof(TService), implementationInstance);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.ReplaceSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            return serviceCollection
                .RemoveScoped(service)
                .AddScoped(service, implementationType);
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceScoped(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            return serviceCollection
                .RemoveScoped(service)
                .AddScoped(service, implementationFactory);
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceScoped<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            return serviceCollection.ReplaceScoped(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.ReplaceScoped(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Replaces multiple services in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="descriptor"><see cref="ServiceDescriptor"/> providing the services.</param>
        public static void ReplaceEnumerable(this IServiceCollection serviceCollection, ServiceDescriptor descriptor)
        {
            CommonValidator.CheckForNullReference(descriptor, nameof(descriptor));
            RemoveServices(serviceCollection, s => s.ServiceType == descriptor.ServiceType && s.Lifetime == descriptor.Lifetime);
            serviceCollection.TryAddEnumerable(descriptor);
        }

        /// <summary>
        /// Replaces multiple services in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="descriptors"><see cref="ServiceDescriptor"/> providing the services.</param>
        public static void ReplaceEnumerable(this IServiceCollection serviceCollection, IEnumerable<ServiceDescriptor> descriptors)
        {
            CommonValidator.CheckForNullReference(descriptors, nameof(descriptors));
            descriptors.ForEach(serviceCollection.ReplaceEnumerable);
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
