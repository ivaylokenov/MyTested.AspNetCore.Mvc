namespace MyTested.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal;
    using Internal.Application;
    using Internal.Caching;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.Caching.Memory;
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
        public static void AddHttpContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds <see cref="IActionContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void AddActionContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
            serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
        
        /// <summary>
        /// Replaces the default <see cref="ITempDataProvider"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceTempDataProvider(this IServiceCollection serviceCollection)
        {
            serviceCollection.ReplaceSingleton<ITempDataProvider, MockedTempDataProvider>();
        }

        /// <summary>
        /// Replaces the default <see cref="IMemoryCache"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceMemoryCache(this IServiceCollection serviceCollection)
        {
            serviceCollection.Replace<IMemoryCache, MockedMemoryCache>(ServiceLifetime.Transient);
        }

        /// <summary>
        /// Removes а service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void Remove(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service);
        }

        /// <summary>
        /// Removes a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void Remove(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType);
        }

        /// <summary>
        /// Removes a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void Remove<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.Remove(typeof(TServive));
        }

        /// <summary>
        /// Removes a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void Remove<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.Remove(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void RemoveTransient(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Transient);
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void RemoveTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Transient);
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveTransient<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.RemoveTransient(typeof(TServive));
        }

        /// <summary>
        /// Removes a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveTransient<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.RemoveTransient(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void RemoveSingleton(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void RemoveSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveSingleton<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.RemoveSingleton(typeof(TServive));
        }

        /// <summary>
        /// Removes a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveSingleton<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.RemoveSingleton(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void RemoveScoped(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void RemoveScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveScoped<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.RemoveScoped(typeof(TServive));
        }

        /// <summary>
        /// Removes a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveScoped<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.RemoveScoped(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Replaces a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void Replace(this IServiceCollection serviceCollection, Type service, Type implementationType, ServiceLifetime lifetime)
        {
            serviceCollection.Remove(service);
            serviceCollection.Add(ServiceDescriptor.Describe(service, implementationType, lifetime));
            TestServiceProvider.SaveServiceLifetime(service, lifetime);
        }

        /// <summary>
        /// Replaces a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void Replace(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            serviceCollection.Remove(service);
            serviceCollection.Add(ServiceDescriptor.Describe(service, implementationFactory, lifetime));
            TestServiceProvider.SaveServiceLifetime(service, lifetime);
        }

        /// <summary>
        /// Replaces service lifetime in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void ReplaceLifetime(this IServiceCollection serviceCollection, Type service, ServiceLifetime lifetime)
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
        }

        /// <summary>
        /// Replaces service lifetime in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void ReplaceLifetime<TService>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TService : class 
        {
            serviceCollection.ReplaceLifetime(typeof(TService), lifetime);
        }

        /// <summary>
        /// Replaces a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void Replace<TService, TImplementation>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.Replace(typeof(TService), typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void ReplaceTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.RemoveTransient(service);
            serviceCollection.AddTransient(service, implementationType);
        }

        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceTransient(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.RemoveTransient(service);
            serviceCollection.AddTransient(service, implementationFactory);
        }

        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceTransient<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.ReplaceTransient(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Replaces a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.ReplaceTransient(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void ReplaceSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.RemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationType);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceSingleton(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.RemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationFactory);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        public static void ReplaceSingleton(this IServiceCollection serviceCollection, Type service, object implementationInstance)
        {
            serviceCollection.RemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationInstance);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceSingleton<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.ReplaceSingleton(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        public static void ReplaceSingleton<TService>(this IServiceCollection serviceCollection, object implementationInstance)
            where TService : class
        {
            serviceCollection.ReplaceSingleton(typeof(TService), implementationInstance);
        }

        /// <summary>
        /// Replaces a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.ReplaceSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void ReplaceScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.RemoveScoped(service);
            serviceCollection.AddScoped(service, implementationType);
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceScoped(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.RemoveScoped(service);
            serviceCollection.AddScoped(service, implementationFactory);
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceScoped<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.ReplaceScoped(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Replaces a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.ReplaceScoped(typeof(TService), typeof(TImplementation));
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
